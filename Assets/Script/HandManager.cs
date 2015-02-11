using System;
using UnityEngine;
using System.Collections;

public class HandManager : MonoBehaviour
{
    private BaseCard selectedCard;

    void Start()
    {

    }

    void Update()
    {
        CheckMouse();
    }

    public void SelectCard(BaseCard card)
    {
        selectedCard = card;
    }

    void CheckMouse()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float fx = worldPos.x + 7.5f;
        int y = (int) Mathf.Floor(worldPos.y - 1.5f) * -1;

        if (y%2 != 0) {
            fx += 0.5f;
        }

        int x = (int) Mathf.Floor(fx);

        // TODO: every second row
        if (0 <= x && x <= 15) {
            if (selectedCard != null && Input.GetMouseButtonDown(0)) {
                if (CanUseSpell(selectedCard, x, y)) {
                    UseCard(selectedCard, x, y);
                }
            }
        }
    }

    //TODO: NYI
    private bool CanUseSpell(BaseCard card, int x, int y)
    {
        return true;
    }

    private void UseCard(BaseCard card, int x, int y)
    {
        card.UseCard(new MapPosition(x, y));
    }
}