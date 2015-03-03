using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Pretty much the opposite of the debuff. Gives +1 max health and health, and +1 attack if it was able to steal it.
/// </summary>
public class ZombieKingBuff : IEffect
{
    private CreatureStats owner;
    private bool stoleAttack;
    private int DurationLeft;
    private const int Duration = 3;

    public ZombieKingBuff(bool stealAttack)
    {
        stoleAttack = stealAttack;

        DurationLeft = Duration;
    }

    ~ZombieKingBuff()
    {
        UnregisterCallbacks();
    }


    public void Removed()
    {
        if (stoleAttack) {
            owner.Attack--;
        }
        owner.Health--;
        owner.MaxHealth--;

        UnregisterCallbacks();
    }

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner.GetComponent<CreatureStats>();
        if (stoleAttack) {
            owner.Attack++;
        }
        owner.Health++;
        owner.MaxHealth++;
    }

    public void InitCallbacks()
    {
        EventManager.OnCreatureStartMovement += OnCreatureStartMovement;
    }

    private void UnregisterCallbacks()
    {
        EventManager.OnCreatureStartMovement -= OnCreatureStartMovement;
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