using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public enum CombatZone
{
    Friendly,
    Neutral,
    Hostile,
}
public static class CombatManager
{
    // Note: 15,y does not exist in every second row
    private static CreatureStats[,] permanentMap = new CreatureStats[15, 5];

    private const float AnimationMoveDuration = 0.35f;
    private const float AnimationAttackDuration = 0.35f;

    /// <summary>
    /// Translates a grid position to the correct world position
    /// </summary>
    /// <param name="pos">Grid position</param>
    /// <returns>World position</returns>
    public static Vector3 GridToWorld(MapPosition pos)
    {
        float x = pos.x;
        float y = pos.y;

        if (pos.y%2 == 0) {
            x -= 0.5f;
        }

        y = -y;

        x -= 6;
        y += 1.5f;

        //permanent.transform.position = new Vector3(x, y);

        //return new Vector3(pos.x -7.5, pos.y-0.5, 0);
        return new Vector3(x, y, 0);
    }

    /// <summary>
    /// Inverse of GridToWorld
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static MapPosition WorldToGrid(Vector3 pos)
    {
        float x = pos.x;
        float y = pos.y;

        x += 6f;
        y -= 1.5f;
        y = -y;

        if ((int)y%2 == 0) {
            x += 0.5f;
        }
        return new MapPosition((int)x, (int)y);
    }

    /// <summary>
    /// Returns what zone the targeted tile is considered to belong to, from faction's perspective.
    /// </summary>
    /// <param name="pos">The tile to check</param>
    /// <param name="faction">The faction to check for</param>
    /// <returns></returns>
    public static CombatZone GetZone(MapPosition pos, Owner faction)
    {
        switch (faction) {
            case Owner.ENEMY:
                if (pos.x >= 14 - 4) {
                    return CombatZone.Friendly;
                }
                else if (pos.x >= 5) {
                    return CombatZone.Neutral;
                }
                else {
                    return CombatZone.Hostile;
                }
            case Owner.PLAYER:
                if (pos.x >= 14 - 4) {
                    return CombatZone.Hostile;
                }
                else if (pos.x >= 5) {
                    return CombatZone.Neutral;
                }
                else {
                    return CombatZone.Friendly;
                }
        }
        throw new Exception("Something seems to have went wrong with combat zones. Unhandled case of new type?");
    }

    // Spawn a new permanent on the map
    public static void SpawnPermanent(GameObject permanentPrefab, Owner caster, MapPosition pos)
    {
        GameObject permanent = GameObject.Instantiate(permanentPrefab) as GameObject;
        CreatureStats stats = permanent.GetComponent<CreatureStats>();

        // Error checking
        if (Utils.OutOfBounds(pos)) {
            Debug.LogError("Error, " + stats.name + " was attempted to be spawned in invalid position: " + pos);
        }

        if (permanentMap[pos.x, pos.y] != null) {
            Debug.LogError("Error, " + stats.name +
                           " was attempted to be spawned on top of another permanentPrefab at: " +
                           pos);
        }

        // Set up stats
        stats.OwnedBy = caster;
        stats.GridPosition = pos;

        //Set the transform position
        permanent.transform.position = GridToWorld(pos);

        // Flip the creature if it is an enemy
        int xScale = stats.OwnedBy == Owner.PLAYER ? 1 : -1;
        Vector3 scale = new Vector3(xScale, 1f, 1f);
        permanent.transform.localScale = scale;

        permanentMap[pos.x, pos.y] = stats;


        EventManager.InvokeCreatureSpawned(stats, pos);
    }

    public static IEnumerator DoCombatPhase(Owner player)
    {
        Stack<CreatureStats> turnOrder = GetTurnOrder(player);

        //Now act on creatures
        foreach (CreatureStats creature in turnOrder) {
            EventManager.InvokeCreatureStartMovement(creature);

            ActPermanent(creature);

            // Now we wait until animations are finished before continuing
            while (StateManager.GetAnimationState() == AnimState.Playing) {
                yield return 0;
            }
        }

        EventManager.InvokeEndOfTurn();
    }

    private static Stack<CreatureStats> GetTurnOrder(Owner player)
    {
        Stack<CreatureStats> turnOrder = new Stack<CreatureStats>();

        // Populate the turnOrder
        // TODO: correct turn order (right -> left -> down for Player, left -> right -> down for enemy
        for (int y = 4; y >= 0; y--) {
            for (int x = 0; x < 15; x++) {
                if (y%2 == 0 && x == 15) {
                    //Skip the non-existing tiles
                    continue;
                }

                // Retrieve the creature in correct order depending on player
                CreatureStats permanent = null;
                switch (player) {
                    case Owner.PLAYER:
                        permanent = permanentMap[x, y];
                        break;
                    case Owner.ENEMY:
                        permanent = permanentMap[15 - 1 - x, y];
                        break;
                }

                if (permanent != null && permanent.OwnedBy == player) {
                    turnOrder.Push(permanent);
                }
            }
        }

        return turnOrder;
    }

    public static CreatureStats GetCreatureAt(MapPosition pos)
    {
        if (Utils.OutOfBounds(pos)) {
            return null;
        }
        return permanentMap[pos.x, pos.y];
    }

    // Does a creature's combat turn
    private static void ActPermanent(CreatureStats permanent)
    {
        if (CanAttackAnything(permanent)) {
            Attack(permanent);
            return;
        }


        if (permanent.CanMove()) {
            int movesLeft = (int)permanent.Speed;

            for (int i = 0; i < movesLeft; i++) {
                if (CanAttackAnything(permanent)) {
                    Attack(permanent);
                    return;
                }
                CreatureStats inFront = GetCreatureAt(permanent.GetForward());
                if (!inFront) {
                    Move(permanent);
                    
                }
            }
        }
    }

    // Do we have a target to attack?
    private static bool CanAttackAnything(CreatureStats permanent)
    {
        // Check if there is a target in front of us
        CreatureStats inFront = GetCreatureAt(permanent.GetForward());

        if (inFront && inFront.OwnedBy != permanent.OwnedBy) {
            return true;
        }
        return false;
    }

    private static void Attack(CreatureStats permanent)
    {
        CreatureStats enemy = permanent.GetAttackTarget();
        enemy.TakeDamage(permanent, permanent.Attack);

        permanent.GetComponent<Animator>().Play("Attack");
        StateManager.RegisterAnimation(AnimationAttackDuration);
    }

    /// <summary>
    /// Moves the permanent based on its movement stats
    /// </summary>
    /// <param name="permanent">Permanent to make move</param>
    private static void Move(CreatureStats permanent)
    {
        MapPosition from = permanent.GridPosition;
        permanentMap[permanent.GridPosition.x, permanent.GridPosition.y] = null;

        MapPosition to = permanent.GetForward();

        permanentMap[to.x, to.y] = permanent;
        permanent.GridPosition = to;

        permanent.GetComponent<Animator>().SetBool("IsWalking", true);

        // Move it and set animating state
        LeanTween.move(permanent.gameObject, GridToWorld(to), AnimationMoveDuration).
            setOnComplete(() =>
            {
                if (!permanent) return;
                permanent.GetComponent<Animator>().SetBool("IsWalking", false);
            });
        
        StateManager.RegisterAnimation(AnimationMoveDuration);
        //permanent.transform.position = GridToWorld(to);

        EventManager.InvokePermanentMoved(permanent, from, to);
    }

    /// <summary>
    /// Removes a permanent from the game. Destroys the gameobject and performs other cleanup actions and event handling
    /// </summary>
    /// <param name="permanent"></param>
    public static void RemovePermanent(CreatureStats permanent)
    {
        EventManager.InvokePermanentDestroyed(permanent);

        GameObject.Destroy(permanent.gameObject);
    }

    /// <summary>
    /// Returns the default tile for a creature to move in. This will always be x+1 or x-1 unless the creature is at the end of the lane and need to advance towards the general by switching lane.
    /// </summary>
    public static MapPosition GetAdvancingMovement(MapPosition position, Owner faction)
    {
        // If we are not at the end of the lane, just move forward.
        if (!Utils.IsAtEndOfLane(position, faction)) {
            switch (faction) {
                case Owner.PLAYER:
                    return position.InDirection(Direction.RIGHT);
                case Owner.ENEMY:
                    return position.InDirection(Direction.LEFT);
            }
        }

        if (position.y <= 2) {
            switch (faction) {
                case Owner.PLAYER:
                    return position.InDirection(Direction.UPLEFT);
                case Owner.ENEMY:
                    return position.InDirection(Direction.UPRIGHT);
            }
        }
        else {
            switch (faction) {
                case Owner.PLAYER:
                    return position.InDirection(Direction.DOWNLEFT);
                case Owner.ENEMY:
                    return position.InDirection(Direction.DOWNRIGHT);
            }
        }
        throw new Exception("Error finding advancing move. Invalid faction? " + faction + ", " + position);
    }

    /// <summary>
    /// Can something spawn a creature here? currently only illegal if there is already a creature there
    /// </summary>
    /// <param name="creaturePrefab"></param>
    /// <param name="owner"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    internal static bool CanSpawn(GameObject creaturePrefab, Owner owner, MapPosition position)
    {
        return !GetCreatureAt(position);
    }
}