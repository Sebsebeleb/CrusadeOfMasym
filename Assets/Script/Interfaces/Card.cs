using UnityEngine;

public enum CardType
{
    CREATURE,
    SPELL,
    EQUIPMENT
}

public abstract class BaseCard : ScriptableObject
{
    public CardType Type;
    public int TargetingLevel;
    public string Description;
    public CreatureStats AssociatedPermanent = null; // The permanent associated with this card for creature and equipment cards

    // Called when a card is succesfully used at a target tile
    abstract public void UseCard(MapPosition target);

}

public class CreatureCard : BaseCard
{
    public override void UseCard(MapPosition target)
    {
        CombatManager.SpawnPermanent(AssociatedPermanent, target);
    }
}