using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


// Behaviour for the gameobject cards
public class CardBehaviour : MonoBehaviour
{

    // The actual card this is associated with which will be used if it is cast
    private BaseCard Card;

    // Refs
    private HandManager handManager;

    public Text NameText;
    public Text DescriptionText;
    public Text TypeText;
    public Image CardArt;

    void Start()
    {
        handManager = GameObject.FindGameObjectWithTag("GM").GetComponent<HandManager>();
    }

    void Update()
    {

    }

    public BaseCard GetBaseCard()
    {
        return Card;
    }

    public void SetCard(CardData cardData)
    {
        switch (cardData.TypeOfCard) {
            case CardType.Creature:
                Card = new CreatureCard(cardData.AssociatedCardEffect, cardData.Description);
                break;
            case CardType.Spell:
                Card = new SpellCard(cardData.AssociatedCardEffect, cardData.Description, cardData.SpellAnimationName);
                break;
            case CardType.Equipment:
                Debug.LogException(new NotImplementedException());
                break;

        }

        NameText.text = Card.Name;
        DescriptionText.text = Card.Description;
        TypeText.text = Card.cardType.ToString();

    }

    // Called by UI event system when this is selected in the hand
    public void Select()
    {
        handManager.SelectCard(this);
    }
}