using UnityEngine;

public static class CombatManager
{
    // Note: 15,y does not exist in every second row
    private static readonly CreatureStats[,] permanentMap = new CreatureStats[15, 5];

    // Spawn a new permanent on the map
    public static void SpawnPermanent(CreatureStats permanent, MapPosition pos)
    {
        if (Utils.OutOfBounds(pos)) {
            Debug.LogError("Error, " + permanent.name + " was attempted to be spawned in invalid position: " + pos);
        }

        if (permanentMap[pos.x, pos.y] != null) {
            Debug.LogError("Error, " + permanent.name + " was attempted to be spawned on top of another permanent at: " +
                           pos);
        }
        permanentMap[pos.x, pos.y] = permanent;
    }

    public static void DoCombatPhase(Owner player)
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
            }
        }
    }


    // Does a creature's combat turn
    private static void ActPermanent(CreatureStats permanent)
    {
    }
}