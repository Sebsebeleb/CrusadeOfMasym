using System;
using UnityEngine;

public class CreatureStats : MonoBehaviour
{
    public Owner OwnedBy;
    private int _hp;
    private int _maxHP;
    private float _speed;
    //Counts how long we have not moved. Only used if speed is lower than 1.0, if it is, this counter will increase by _speed each turn, and the creature will only move if it has speedcounter >= 1.0 and the counter is then reset
    private float _speedCounter;
    public String name;

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
        set { _hp = Math.Min(value, _maxHP); }
    }

    // If speed is higher than 1.0, it is always rounded down.
    public float Speed
    {
        get
        {
            if (Speed >= 1.0) {
                return Mathf.Floor(_speed);
            }

            return _speed;
        }
        set { _speed = value; }
    }

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}