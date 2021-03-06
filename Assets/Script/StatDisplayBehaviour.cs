﻿using UnityEngine;
using UnityEngine.UI;

public class StatDisplayBehaviour : MonoBehaviour
{
    private CreatureStats creature;

    public Text HealthText;
    public Text AttackText;
    public Text DefenceText;

    private const float valueLerpTime = 0.5f;
    private LeanTweenType tweenType = LeanTweenType.easeOutExpo;

    private int oldHealth;
    private int oldAttack;
    private int oldDefence;

    private void Update()
    {
        SetHealth(creature.GetHealth());
        SetDefence(creature.Defense);
        SetAttack(creature.Attack);

        //LeanTween.value(HealthText.gameObject, SetTextValue)
    }

    private void SetHealth(int value)
    {
        if (value == oldHealth)
        {
            return;
        }

        LeanTween.value(gameObject, delegate(float f)
        {
            HealthText.text = (Mathf.Round(f)).ToString();
        },
            oldHealth,
            value,
            valueLerpTime
            );

        oldHealth = value;
    }

    private void SetAttack(int value)
    {
        if (value == oldAttack) return;

        LeanTween.value(gameObject, delegate(float f, object o) { AttackText.text = (Mathf.Round(f)).ToString(); },
            oldAttack,
            value,
            valueLerpTime
            ).setEase(tweenType);

        oldAttack = value;
    }

    private void SetDefence(int value)
    {
        if (oldDefence == value) return;
        LeanTween.value(gameObject, delegate(float f, object o) { DefenceText.text = (Mathf.Round(f)).ToString(); },
            oldDefence,
            value,
            valueLerpTime
            );

        oldDefence = value;
    }


    public void SetOwner(CreatureStats parent)
    {
        creature = parent;
    }
}