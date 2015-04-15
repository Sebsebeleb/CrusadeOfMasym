using System;
using Assets.Script;
using UnityEngine;

[NamedEffect("vampireeffect")]
public class VampireEffect : IEffect
{
    private CreatureStats owner;

    public void Removed()
    {
    }

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner.GetComponent<CreatureStats>();
    }

    public void InitCallbacks()
    {
        EventManager.OnPermanentDestroyed += OnPermanentDestroyed;
    }

    private void OnPermanentDestroyed(CreatureStats creature, Source killSource)
    {
        if (killSource.Creature == owner) {
            owner.Heal(3);
        }
    }
}