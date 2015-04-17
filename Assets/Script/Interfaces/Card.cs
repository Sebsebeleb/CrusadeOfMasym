using System;
using UnityEngine;

public enum CardType
{
    Creature,
    Spell,
    Equipment,

}

public enum TargetType
{
    Single,
    Multiple,
}

public abstract class BaseCard
{
    public CardType cardType;
    public int TargetingLevel;
    public string Description;
    public string Name;
    public string AnimationName;
    public Sprite CardArt;

    public TargetType Targeting = TargetType.Single;

    public GameObject AssociatedPermanent = null; // The permanent associated with this card for creature and equipment cards

    // Called when a card is succesfully used at a target tile
    abstract public void UseCard(Owner caster, MapPosition target);

}

public class CreatureCard : BaseCard
{
    public new int TargetingLevel = 1;


    public CreatureCard(String name, String description)
    {
        cardType = CardType.Creature;

        Name = name;
        Description = description;
        AssociatedPermanent = DataLibrary.GetCreatureFromName(Name);
    }
    public override void UseCard(Owner caster, MapPosition target)
    {
        CombatManager.SpawnPermanent(AssociatedPermanent, caster, target);
    }
}

public class SpellCard : BaseCard
{
    public new int TargetingLevel = 3;

    // The effect that will be used when this card is used
    public string AssociatedEffect;


    public SpellCard(string spell, string description, string castAnimationName)
    {
        cardType = CardType.Spell;

        Name = spell;
        AssociatedEffect = spell;
        Description = description;
        AnimationName = castAnimationName;
    }

    public override void UseCard(Owner caster, MapPosition target)
    {
        ISpellEffect effect = DataLibrary.GetEffect(AssociatedEffect) as ISpellEffect;
        effect.OnUseCard(caster, target);
    }
}