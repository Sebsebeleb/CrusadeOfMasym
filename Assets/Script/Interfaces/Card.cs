using System;
using System.Security.Cryptography;
using UnityEngine;

public enum CardType
{
    Creature,
    Spell,
    Equipment,

}

public abstract class BaseCard
{
    public CardType Type;
    public int TargetingLevel;
    public string Description;
    public string Name;

    public GameObject AssociatedPermanent = null; // The permanent associated with this card for creature and equipment cards

    // Called when a card is succesfully used at a target tile
    abstract public void UseCard(MapPosition target);

}

public class CreatureCard : BaseCard
{

    public CreatureCard(String name, String description)
    {
        Name = name;
        Description = description;
        AssociatedPermanent = CreatureLibrary.GetFromName(Name);
    }
    public override void UseCard(MapPosition target)
    {
        CombatManager.SpawnPermanent(AssociatedPermanent, target);
    }
}