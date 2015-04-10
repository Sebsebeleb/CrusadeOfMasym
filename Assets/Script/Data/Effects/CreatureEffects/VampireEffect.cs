using UnityEngine;
using System.Collections;

[NamedEffect("vampireeffect")]
public class VampireEffect : IEffect
{
    private CreatureStats owner;
    private EffectHolder OwnerEffects;

    public void Removed()
    {
    }

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner.GetComponent<CreatureStats>();
        OwnerEffects = newOwner.GetComponent<EffectHolder>();
    }

    public void InitCallbacks()
    {
        EventManager.OnCreatureStartMovement += OnCreatureStartMovement;
    }

    private void OnCreatureStartMovement(CreatureStats creature)
    {
        if (creature == owner) {
            DoSteal();
        }
    }

    private void DoSteal()
    {
        foreach (MapPosition position in Utils.GetAdjacent(owner.GridPosition)) {
            CreatureStats creature = CombatManager.GetCreatureAt(position);
            if (!creature) continue;

            if (creature.OwnedBy != owner.OwnedBy) {
                StealFrom(creature);
            }
        }
    }

    private void StealFrom(CreatureStats creature)
    {
        bool stolenAttack = creature.Attack >= 1;


        ZombieKingDebuff debuff = new ZombieKingDebuff(stolenAttack);
        creature.GetComponent<EffectHolder>().AddEffect(debuff);

        ZombieKingBuff buff = new ZombieKingBuff(stolenAttack);
        OwnerEffects.AddEffect(buff);
    }
}