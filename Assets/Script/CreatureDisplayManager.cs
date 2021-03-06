﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Currently adds healthbar displays to spawned creatures
/// </summary>
public class CreatureDisplayManager : MonoBehaviour
{

    public GameObject StatDisplayPrefab;

    private void Awake()
    {
        EventManager.OnCreatureSpawned += onCreatureSpawned;
    }

    private void onCreatureSpawned(CreatureStats creature, MapPosition position)
    {
        GameObject newDisplay = Instantiate(StatDisplayPrefab) as GameObject;

        newDisplay.transform.SetParent(creature.transform, true);
        newDisplay.GetComponent<StatDisplayBehaviour>().SetOwner(creature);
        newDisplay.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, -0.5f, -0.5f);
    }
}