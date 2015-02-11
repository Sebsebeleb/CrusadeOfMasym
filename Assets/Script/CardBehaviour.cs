using UnityEngine;
using System.Collections;
using UnityEngine.UI;


// Behaviour for the gameobject cards
public class CardBehaviour : MonoBehaviour
{

    // The actual card this is associated with which will be used if it is cast
    public BaseCard Card;

    // Refs
    private HandManager handManager;

    public Text NameText;
    public Text DescriptionText;
    public Text TypeText;

    void Start()
    {
        handManager = GameObject.FindGameObjectWithTag("GM").GetComponent<HandManager>();
    }

    void Update()
    {

    }

    public void SetCard(BaseCard card)
    {
        Card = card;

        NameText.text = Card.Name;
        DescriptionText.text = Card.Description;
        TypeText.text = Card.Type.ToString();
    }

    // Called by UI event system when this is selected in the hand
    public void Select()
    {
        handManager.SelectCard(Card);
    }
}