using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CombatManager
{
    // Note: 15,y does not exist in every second row
    private static CreatureStats[,] permanentMap = new CreatureStats[15, 5];

    /// <summary>
    /// Translates a grid position to the correct world position
    /// </summary>
    /// <param name="pos">Grid position</param>
    /// <returns>World position</returns>
    public static Vector3 GridToWorld(MapPosition pos)
    {
        float x = pos.x;
        float y = pos.y%2 == 0 ? pos.y : pos.y - 0.5f;

        y = -y;

        x -= 6;
        y += 2;

        //permanent.transform.position = new Vector3(x, y);

        //return new Vector3(pos.x -7.5, pos.y-0.5, 0);
        return new Vector3(x, y, 0);
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
        //Set the position
        permanent.transform.position = GridToWorld(pos);

        permanentMap[pos.x, pos.y] = stats;

        stats.OwnedBy = caster;
        stats.GridPosition = pos;
        
        EventManager.InvokeCreatureSpawned(stats, pos);
    }

    public static IEnumerator DoCombatPhase(Owner player)
    {

        Stack<CreatureStats> turnOrder = new Stack<CreatureStats>();


        // Populate the turnOrder
        // TODO: correct turn order (right -> left -> down for Player, left -> right -> down for enemy
        for (int x = 0; x < 15; x++) {
            for (int y = 0; y < 5; y++) {
                if (y%2 == 0 && x == 15) {
                    //Skip the non-existing tiles
                    continue;
                }
                CreatureStats permanent = permanentMap[x, y];
                if (permanent != null && permanent.OwnedBy == player) {
                    turnOrder.Push(permanent);
                }
            }
        }

        //Now act on creatures
        foreach (CreatureStats creature in turnOrder) {
            EventManager.InvokeCreatureStartMovement(creature);

            ActPermanent(creature);


            // Now we wait until animations are finished before continuing
            if (StateManager.GetAnimationState() == AnimState.Playing) {
                yield return 0;
            }
        }
    }

    public static CreatureStats GetCreatureAt(MapPosition pos)
    {
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
            int movesLeft = Math.Min(1, (int) Mathf.Round(permanent.Speed));

            for (int i = 0; i < movesLeft; i++) {
                Move(permanent);
            }
        }
    }

    // Do we have a target to attack?
    private static bool CanAttackAnything(CreatureStats permanent)
    {
        // Check if there is a target in front of us
        CreatureStats inFront = GetCreatureAt(permanent.GetForward());

        if (inFront) {
            return true;
        }
        return false;
    }

    private static void Attack(CreatureStats permanent)
    {
        CreatureStats enemy = permanent.GetAttackTarget();
        enemy.TakeDamage(permanent, permanent.Attack);
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

        permanent.transform.position = GridToWorld(to);

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
}