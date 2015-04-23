using System.Security.AccessControl;
using Assets.Script;
using UnityEngine;

public class GeneralCreatureBehaviour : MonoBehaviour
{
    private CreatureStats stats;
    private GameObject GM;

    private void Start()
    {
        GM = GameObject.FindWithTag("GM");

        stats = GetComponent<CreatureStats>();
        stats.MakeImobile();

        // Make them able to attack everywhere
        Direction[] dirs = {Direction.DOWNLEFT, Direction.DOWNRIGHT, Direction.UPLEFT, Direction.UPRIGHT};

        foreach (Direction dir in dirs) {
            stats.AddAttackDirection(dir);
        }
        

        EventManager.OnPermanentDestroyed += OnPermanentDestroyed;
    }

    private void Update()
    {
    }

    public void OnPermanentDestroyed(CreatureStats deadCreature, Source killSource)
    {
        if (deadCreature == stats)
        {
            LoseGame();
        }
    }

    private void LoseGame()
    {
        Owner winner = Owner.PLAYER;
        if (stats.OwnedBy == Owner.PLAYER)
        {
            winner = Owner.ENEMY;
        }
        GM.GetComponent<GameManager>().EndGame(winner);
    }
}