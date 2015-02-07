using System;
using System.Collections;
using UnityEngine;

public static class CombatManager
{
    // Note: 15,y does not exist in every second row
    private static CreatureStats[,] permanentMap = new CreatureStats[15, 5];

    // Spawn a new permanent on the map
    public static void SpawnPermanent(CreatureStats permanent, MapPosition pos)
    {
        // Debug stuff
        if (Utils.OutOfBounds(pos)) {
            Debug.LogError("Error, " + permanent.name + " was attempted to be spawned in invalid position: " + pos);
        }

        if (permanentMap[pos.x, pos.y] != null) {
            Debug.LogError("Error, " + permanent.name + " was attempted to be spawned on top of another permanent at: " +
                           pos);
        }
        //Set the position
        float x = pos.x;
        float y = pos.y%2 == 0 ? pos.y : pos.y - 0.5f;
        permanent.transform.position = new Vector3(x, y);

        permanentMap[pos.x, pos.y] = permanent;
    }

    public static IEnumerator DoCombatPhase(Owner player)
    {
        for (int x = 0; x <= 5; x++) {
            for (int y = 0; y <= 15; y++) {
                if (y%2 == 0 && x == 15) {
                    //Skip the non-existing tiles
                    continue;
                }
                CreatureStats permanent = permanentMap[x, y];
                if (permanent != null && permanent.OwnedBy == player) {
                    ActPermanent(permanent);
                }
                // Now we wait until animations are finished before continuing
                if (StateManager.GetAnimationState() == AnimState.Playing) {
                    yield return 0;
                }
            }
        }
    }


    // Does a creature's combat turn
    private static void ActPermanent(CreatureStats permanent)
    {
        if (CanAttackAnything(permanent)) {
            Attack(permanent);
        }

        if (permanent.CanMove()) {
            int movesLeft = Math.Min(1, (int) Mathf.Round(permanent.Speed));

            for (int i = 0; i < movesLeft; i++) {
                Move(permanent);
            }
        }
    }

    private static bool CanAttackAnything(CreatureStats permanent)
    {
        return false;
    }

    private static void Attack(CreatureStats permanent)
    {

    }

    private static void Move(CreatureStats permanent)
    {

    }
}