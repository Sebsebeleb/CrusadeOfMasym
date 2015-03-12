using UnityEngine;

/// <summary>
/// Used to define cards in Unity and act as a glue between display and control.
/// </summary>
public class CardData : MonoBehaviour
{
    public string CardName;
    public string Description;
    public Sprite CardImage;
    public CardType TypeOfCard;
    public int TargetingLevel;

    [HideInInspector]
    public string SpellAnimationName; // Only used for spell cards, exposed by the custom inspector

    // For spells, this should be the name of the effect to be used when the card is used. For creatures it's the creature name, etc.
    public string AssociatedCardEffect;
}
