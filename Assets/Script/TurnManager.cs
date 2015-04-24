using System;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static Owner CurrentPlayer;

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
        List<string> playerExampleCards = new List<string>();
        List<string> enemyExampleCards = new List<string>();


        for (int i = 0; i < 10; i++)
        {

            playerExampleCards.Add("Human Phalanx");
            playerExampleCards.Add("Sleep");
            playerExampleCards.Add("Undead Vampire");
            playerExampleCards.Add("Human Priest");
            playerExampleCards.Add("Divine Light");
            playerExampleCards.Add("Consecration");
            enemyExampleCards.Add("Meteor");
            enemyExampleCards.Add("Fury");
            enemyExampleCards.Add("Holy Bolt");
            enemyExampleCards.Add("Human Cleaver");
            enemyExampleCards.Add("Zombie King");
            enemyExampleCards.Add("Human Pikeman");
        }

        PlayerDeck = new Deck(playerExampleCards);
        EnemyDeck = new Deck(enemyExampleCards);

        PlayerDeck.Shuffle();
        EnemyDeck.Shuffle();
    }

    public void DrawStep()
    {
        Deck currentDeck = null;
        Transform currentHand = null;
        switch (CurrentPlayer)
        {
            case Owner.PLAYER:
                currentDeck = PlayerDeck;
                currentHand = PlayerHand;
                break;
            case Owner.ENEMY:
                currentDeck = EnemyDeck;
                currentHand = EnemyHand;
                break;
            default:
                Debug.LogException(new Exception("Something went wrong during drawstep"));
                break;

        }

        int toDraw = 7 - currentHand.transform.childCount;

        for (int i = toDraw; i > 0; i--)
        {
            string drawn = currentDeck.DrawCard();
            GameObject physicalCard = MakePhysicalCard(drawn);
            physicalCard.transform.SetParent(currentHand.transform);

        }
    }

    private GameObject MakePhysicalCard(string card)
    {
        // Find the actual card by the given name
        CardData data = DataLibrary.LoadCardFromString(card);

        GameObject newCard = Instantiate(CardPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        CardBehaviour behaviour = newCard.GetComponent<CardBehaviour>();

        behaviour.SetCard(data);

        return newCard;
    }

    public void DiscardStep()
    {

    }

    public void NewTurn()
    {
        if (!CanEndTurn())
        {
            return;
        }
        StartCoroutine(CombatManager.DoCombatPhase(CurrentPlayer));

        EventManager.InvokeEndOfTurn();

        if (CurrentPlayer == Owner.ENEMY)
        {
            EnemyHand.gameObject.SetActive(false);
            PlayerHand.gameObject.SetActive(true);
        }
        else
        {
            PlayerHand.gameObject.SetActive(false);
            EnemyHand.gameObject.SetActive(true);
        }

        // Change current player
        CurrentPlayer = (CurrentPlayer == Owner.PLAYER) ? Owner.ENEMY : Owner.PLAYER;

        DrawStep();

    }

    private bool CanEndTurn()
    {
        if (StateManager.GetAnimationState() != AnimState.Idle)
        {
            return false;
        }

        return true;
    }
}
