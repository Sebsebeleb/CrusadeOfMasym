
[NamedEffect("Fury")]
internal class Fury : ISpellEffect
{
    public void OnUseCard(Owner caster, MapPosition target)
    {
        CreatureStats creature = CombatManager.GetCreatureAt(target);
        if (creature)
        {
            FuryBuff buff = new FuryBuff();
            creature.GetComponent<EffectHolder>().AddEffect(buff);
        }
    }

    public void Removed()
    {
    }

    public void SetOwner(UnityEngine.GameObject owner)
    {
    }

    public void InitCallbacks()
    {
    }
}