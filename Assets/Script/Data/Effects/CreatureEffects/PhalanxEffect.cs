using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[NamedEffect("phalanxeffect")]
public class PhalanxEffect : IEffect
{
    private GameObject owner;
    private CreatureStats ownerStats;

    private int currentBonus = 0;

    public void InitCallbacks()
    {
        EventManager.OnPermanentMoved += OnPermanentMoved;
        EventManager.OnPermanentDestroyed += OnPermanentDestroyed;
        EventManager.OnCreatureSpawned += OnCreatureSpawned;
    }

    public void Removed()
    {
        // Remove the bonuses granted by the trait
        ownerStats.Attack -= currentBonus*2;
        ownerStats.Defense -= currentBonus;
    }

    public void SetOwner(GameObject creature)
    {
        owner = creature;
        ownerStats = creature.GetComponent<CreatureStats>();
    }

    private void OnPermanentMoved(CreatureStats mover, MapPosition from, MapPosition to)
    {
        CalculateBonus();
    }

    private void OnPermanentDestroyed(CreatureStats permanent)
    {
        CalculateBonus();
    }

    private void OnCreatureSpawned(CreatureStats permanent, MapPosition at)
    {
        CalculateBonus();
    }

    private void CalculateBonus()
    {
        int numadjacent= 0;
        // Check for adjacent allies
        foreach (MapPosition position in Utils.GetAdjacent(ownerStats.GridPosition)) {
            CreatureStats adjCreature = CombatManager.GetCreatureAt(position);
            if (adjCreature != null && adjCreature.OwnedBy == ownerStats.OwnedBy) {
                numadjacent++;
            }
        }

        int diff =  numadjacent - currentBonus;

        ownerStats.Attack += diff*2;
        ownerStats.Defense += diff;

        currentBonus = numadjacent;
    }
}