using Assets.Script;
using UnityEngine;
using System.Collections;

class FuryBuff : TemporaryEffect
{
    private CreatureStats ownerStats;
    private int DurationLeft;

    public override void Removed()
    {
        base.Removed();

        ownerStats.Speed -= 2;
        ownerStats.Attack -= 3;
    }

    public override void SetOwner(UnityEngine.GameObject owner)
    {
        ownerStats = owner.GetComponent<CreatureStats>();
        ownerStats.Speed += 2;
        ownerStats.Attack += 3;

        DurationLeft = 3;

    }

    public override void InitCallbacks()
    {
        EventManager.OnCreatureStartMovement += OnTurn;
    }

    protected override void UnregisterCallbacks()
    {
        EventManager.OnCreatureStartMovement -= OnTurn;
    }

    private void OnTurn(CreatureStats creature)
    {
        if (creature != ownerStats)
        {
            return;
        }

        DurationLeft--;
        if (DurationLeft <= 0)
        {
            ownerStats.GetComponent<EffectHolder>().RemoveEffect(this);
        }

        creature.TakeDamage(new Source(creature), new Damage(3, DamageType.True));
    }
}