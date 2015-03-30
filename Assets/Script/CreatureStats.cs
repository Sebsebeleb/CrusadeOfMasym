using System;
using System.Collections;
using UnityEngine;
using Assets.Script;
using UnityEngine.UI;

public class CreatureStats : MonoBehaviour
{
    public Owner OwnedBy;
    private int _hp;
    private int _maxHP;
    private float _speed = 1;
    //Counts how long we have not moved. Only used if speed is lower than 1.0, if it is, this counter will increase by _speed each turn, and the creature will only move if it has speedcounter >= 1.0 and the counter is then reset
    private float _speedCounter = 0;
    public String name;
    private bool isImobile = false; // If we are, we can never move on our own

    public int Attack;
    public int Defense;
    public int StartMaxHealth;
    public int StartSpeed;

    //Can the creature act during combat phases?
    private int _canAct;
    public bool CanAct
    {
        get { return _canAct >= 0; }
    }

    public MapPosition GridPosition;


    public int MaxHealth
    {
        get { return _maxHP; }
        set
        {
            _maxHP = value;

            //HP cannot be greater than max hp, so we lower it.
            _hp = Math.Min(_hp, _maxHP);
        }
    }

    public int Health
    {
        get { return _hp; }
        set
        {
            _hp = Math.Min(value, _maxHP);
            if (_hp <= 0) {
                Die();
            }
        }
    }

    // If speed is higher than 1.0, it is always rounded down.
    public float Speed
    {
        get
        {
            if (_speed >= 1.0) {
                return Mathf.Floor(_speed);
            }

            return _speed;
        }
        set { _speed = value; }
    }

    public void Start()
    {
        _maxHP = StartMaxHealth;
        _hp = _maxHP;
        Speed = StartSpeed;
    }

    public bool CanMove()
    {
        if (isImobile) {
            return false;
        }
        if (Speed >= 1) {
            return true;
        }

        // If speed is less than 1 we only move every x turn
        if (Speed > 0 && _speedCounter >= 1.0f) {
            return true;
        }
        return false;
    }

    public void MakeImobile()
    {
        isImobile = true;
    }

    /// <summary>
    /// Remove the ability to act or restore it.
    /// </summary>
    /// <param name="restore">true for restore, otherwise remove</param>
    public void SetCanAct(bool restore)
    {
        if (restore) {
            _canAct++;
        }
        else {
            _canAct--;
        }
    }

    /// <summary>
    /// Deal damage to this permanent
    /// </summary>
    /// <param name="damageSource">The attacker or other sort of source</param>
    /// <param name="damage">The amount of damage to take</param>
    /// <returns>The actual damage taken</returns>
    public int TakeDamage(Source damageSource, Damage damage)
    {
        int finalDamage = damage.Value;

        // If the damage is physical, block it using defense
        if (damage.damageType == DamageType.Physical)
        {
            finalDamage -= Defense;
            
        }
        Health -= finalDamage;

        return finalDamage;
    }

    public void Heal(int healAmount)
    {
        Health += healAmount;
    }

    // Returns a MapPosition that represents directly front of this creature
    public MapPosition GetForward()
    {
        return CombatManager.GetAdvancingMovement(GridPosition, OwnedBy);
    }

    // Returns the target to attack if we decide to attack.
    // TODO: Possibility of multiple targets, should return the most prioritised target
    public CreatureStats GetAttackTarget()
    {
        return CombatManager.GetCreatureAt(GetForward());
    }

    public void Die()
    {
        CombatManager.RemovePermanent(this);
    }
}