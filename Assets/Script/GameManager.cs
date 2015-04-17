using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages initalization of stuff
/// </summary>
public class GameManager : MonoBehaviour
{

    public Text WinText;

    void Start()
    {
        DataLibrary.LoadEffects();
        MakeGenerals();
    }

    private void MakeGenerals()
    {
        GameObject generalPrefab = DataLibrary.GetCreatureFromName("General");
        CombatManager.SpawnPermanent(generalPrefab, Owner.PLAYER, new MapPosition(0, 2));
        CombatManager.SpawnPermanent(generalPrefab, Owner.ENEMY, new MapPosition(13, 2));
    }

    void Update()
    {

    }

    public void EndGame(Owner winner)
    {
        WinText.transform.parent.gameObject.SetActive(true);
        WinText.text = (winner.ToString().ToLower() + " wins!");
    }
}