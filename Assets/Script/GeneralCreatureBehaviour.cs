using UnityEngine;
using System.Collections;

public class GeneralCreatureBehaviour : MonoBehaviour
{
    private CreatureStats stats;
    private GameObject GM;

    private void Start()
    {
        GM = GameObject.FindWithTag("GM");

        stats = GetComponent<CreatureStats>();
        stats.MakeImobile();

        EventManager.OnPermanentDestroyed += OnPermanentDestroyed;
    }

    private void Update()
    {
    }

    public void OnPermanentDestroyed(CreatureStats deadCreature)
    {
        if (deadCreature == stats) {
            LoseGame();
        }
    }

    private void LoseGame()
    {
        Owner winner = Owner.PLAYER;
        if (stats.OwnedBy == Owner.PLAYER) {
            winner = Owner.ENEMY;
        }
        GM.GetComponent<GameManager>().EndGame(winner);
    }
}