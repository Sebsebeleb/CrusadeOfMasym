using System;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Collections;

[System.Serializable]
public class CreatureEntry
{
    public GameObject Creature;
    public string Name;
}


public static class CreatureLibrary
{

    private const string ResourcePath = "Creatures/";

    // Used to input creatures in the inspector, they are then parsed into the CreatureMap dictionary at the start of the game
    private static Dictionary<string, GameObject> CreatureMap = new Dictionary<string, GameObject>();


    private static void LoadCreature(string creatureName)
    {
        GameObject creature = Resources.Load<GameObject>(ResourcePath + creatureName);
            CreatureMap[creatureName] = creature;
    }

    public static GameObject GetFromName(string creatureName){
        // Try to load if not cached
        if (!CreatureMap.ContainsKey(creatureName)) {
            LoadCreature(creatureName);
        }
        
        return CreatureMap[creatureName];
    }
}