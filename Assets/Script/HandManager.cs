using System;
using UnityEngine;
using System.Collections;

public class HandManager : MonoBehaviour
{
    public GameObject AnimationDummyPrefab; // Used to play one-shot animations
    private CardBehaviour selectedCard;

    private void Start()
    {
    }

    private void Update()
    {
        CheckMouse();
    }

    public void SelectCard(CardBehaviour card)
    {
        selectedCard = card;
    }

    private void CheckMouse()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //float fx = worldPos.x + 7.5f;
        //int y = (int) Mathf.Floor(worldPos.y - 1.5f)*-1;

        //if (y%2 != 0) {
        //    fx += 0.5f;
        //}

        //int x = (int) Mathf.Floor(fx);

        MapPosition gridPosition = CombatManager.WorldToGrid(worldPos);

        int x = gridPosition.x;
        int y = gridPosition.y;


        // TODO: every second row
        if (0 <= x && x <= 15) {
            if (selectedCard != null && Input.GetMouseButtonDown(0)) {
                if (CanUseSpell(selectedCard.GetBaseCard(), x, y)) {
                    UseCard(selectedCard.GetBaseCard(), x, y);
                    Destroy(selectedCard.gameObject);
                }
                // Deselect card on failed attempt for now
                selectedCard = null;
            }
        }
    }

    private bool CanUseSpell(BaseCard card, int x, int y)
    {
        // We cannot spawn creatures on top of other creatures
        if (card.cardType == CardType.Creature) {
            if (CombatManager.GetCreatureAt(new MapPosition(x, y)) != null) {
                return false;
            }
        }
        return true;
    }

    private void UseCard(BaseCard card, int x, int y)
    {
        card.UseCard(TurnManager.CurrentPlayer, new MapPosition(x, y));

        if (card.AnimationName != null) {
            GameObject dummy = Instantiate(AnimationDummyPrefab);
            Animator dummyAnimator = dummy.GetComponent<Animator>();

            dummyAnimator.Play(card.AnimationName);

            dummy.transform.position = CombatManager.GridToWorld(new MapPosition(x, y));
        }
    }
}