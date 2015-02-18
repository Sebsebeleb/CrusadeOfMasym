using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatDisplayBehaviour : MonoBehaviour
{
    private CreatureStats creature;

    public Text HealthText;
    public Text AttackText;
    public Text DefenceText;

    void Update()
    {
        HealthText.text = creature.Health.ToString();
        AttackText.text = creature.Attack.ToString();
        DefenceText.text = creature.Defense.ToString();
    }

    public void SetOwner(CreatureStats parent)
    {
        creature = parent;
    }
}