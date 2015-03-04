using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ZombieKingDebuff : IEffect
{
    private CreatureStats owner;
    private bool stoleAttack;

    private int DurationLeft;
    private const int Duration = 3; // Amount of turns to persist

    public ZombieKingDebuff(bool stealAttack)
    {
        stoleAttack = stealAttack;

        DurationLeft = Duration;
    }

    public void Removed()
    {
        if (stoleAttack) {
            owner.Attack++;
        }
        owner.MaxHealth++;
        owner.Health++;
    }

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner.GetComponent<CreatureStats>();
        if (stoleAttack) {
            owner.Attack--;
        }
        owner.Health--;
        owner.MaxHealth--;
    }

    public void InitCallbacks()
    {
        EventManager.OnCreatureStartMovement += OnCreatureStartMovement;
    }

    private void OnCreatureStartMovement(CreatureStats creature)
    {
        if (creature == owner) {
            DoTurn();
        }
    }

    private void DoTurn()
    {
        DurationLeft--;
        if (DurationLeft <= 0) {
            owner.GetComponent<EffectHolder>().RemoveEffect(this);
        }
    }
}