using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[NamedEffect("scoundreleffect")]
public class ScoundrelEffect : IEffect
{
    private GameObject ownerGameObject;
    private CreatureStats ownerStats;

    private bool hasBonus = false;

    public void Removed()
    {
    }

    public void SetOwner(UnityEngine.GameObject owner)
    {
        ownerGameObject = owner;
        ownerStats = owner.GetComponent<CreatureStats>();
    }

    public void InitCallbacks()
    {
        EventManager.OnPermanentMoved += OnPermanentMoved;
    }

    private void OnPermanentMoved(CreatureStats mover, MapPosition from, MapPosition to)
    {
        // Was it me that moved?
        if (mover == ownerStats) {
            CheckZoneBonus();
        }
    }

    // Check if we should recieve the +4 attack bonus for being in hostile zone
    private void CheckZoneBonus()
    {
        if (CombatManager.GetZone(ownerStats.GridPosition, ownerStats.OwnedBy) == CombatZone.Hostile) {
            if (!hasBonus) {
                ownerStats.Attack += 4;
            }
            hasBonus = true;
        }
        else {
            if (hasBonus) {
                ownerStats.Attack -= 4;
            }
            hasBonus = false;
        }
    }
}
