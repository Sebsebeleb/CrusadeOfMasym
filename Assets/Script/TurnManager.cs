using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private Owner CurrentPlayer;

    public Deck PlayerDeck;
    public Deck EnemyDeck;

    // Refs to hand gameobjects
    public Transform PlayerHand;
    public Transform EnemyHand;

    // The physical card GameObject prefab
    public GameObject CardPrefab;

    void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        CurrentPlayer = Owner.ENEMY; // Control is changed in NewTurn()
        PopulateDecks();
        NewTurn();
    }

    // Currently sets the decks of both players to a test deck
    private void PopulateDecks()
    {
        List<BaseCard> playerExampleCards = new List<BaseCard>(); 
        List<BaseCard> enemyExampleCards = new List<BaseCard>(); 


        for (int i = 0; i < 20; i++) {
            BaseCard testCard = new CreatureCard("Human Priest", "Test");
            playerExampleCards.Add(testCard);

            BaseCard testCard2 = new CreatureCard("Human Priest", "Test");
            enemyExampleCards.Add(testCard2);

        }

        PlayerDeck = new Deck(playerExampleCards);
        EnemyDeck = new Deck(enemyExampleCards);

        PlayerDeck.Shuffle();
        EnemyDeck.Shuffle();
    }

    public void DrawStep()
    {
        Deck currentDeck = null;
        Transform CurrentHand = null;
        switch (CurrentPlayer) {
            case Owner.PLAYER:
                currentDeck = PlayerDeck;
                CurrentHand = PlayerHand;
                break;
            case Owner.ENEMY:
                currentDeck = EnemyDeck;
                CurrentHand = EnemyHand;
                break;
            default:
                Debug.LogException(new Exception("Something went wrong during drawstep"));
                break;

        }

        int toDraw =  7 - PlayerHand.transform.childCount;

        for (int i = toDraw; i > 0; i--) {
            Debug.Log(currentDeck);
            BaseCard drawn = currentDeck.DrawCard();
            GameObject physicalCard = MakePhysicalCard(drawn);
            physicalCard.transform.SetParent(CurrentHand.transform);

        }
    }

    private GameObject MakePhysicalCard(BaseCard card)
    {
        GameObject newCard = Instantiate(CardPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        CardBehaviour behaviour = newCard.GetComponent<CardBehaviour>();

        behaviour.SetCard(card);

        return newCard;
    }

    public void DiscardStep()
    {
        
    }

    public void NewTurn()
    {
        if (CurrentPlayer == Owner.ENEMY) {
            EnemyHand.gameObject.SetActive(false);
            EnemyHand.gameObject.SetActive(true);
        }
        else {
            PlayerHand.gameObject.SetActive(false);
            PlayerHand.gameObject.SetActive(true);
        }

        // Change current player
        CurrentPlayer = (CurrentPlayer == Owner.PLAYER) ? Owner.ENEMY : Owner.PLAYER;

        DrawStep();
    }
}
