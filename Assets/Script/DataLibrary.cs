using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.Collections;


[System.Serializable]
public class CreatureEntry
{
    public GameObject Creature;
    public string Name;
}


public static class DataLibrary
{
    private const string ResourcePath = "Creatures/";
    private const string CardPath = "Cards/";

    // Used to input creatures in the inspector, they are then parsed into the CreatureMap dictionary at the start of the game
    private static Dictionary<string, GameObject> CreatureMap = new Dictionary<string, GameObject>();
    private static Dictionary<string, Type> effectsMap = new Dictionary<string, Type>();


    private static void LoadCreature(string creatureName)
    {
        GameObject creature = Resources.Load<GameObject>(ResourcePath + creatureName);
        CreatureMap[creatureName] = creature;
    }

    public static GameObject GetCreatureFromName(string creatureName)
    {
        // Try to load if not cached
        if (!CreatureMap.ContainsKey(creatureName)) {
            LoadCreature(creatureName);
        }

        return CreatureMap[creatureName];
    }

    /// <summary>
    /// Loads all defined effects into the game.
    /// </summary>
    public static void LoadEffects()
    {
        Assembly assembly = Assembly.GetCallingAssembly();

        // Find all classes that use the NamedEffectAttribute, and add them to our effect library
        foreach (Type type in assembly.GetTypes()) {
            if (!Attribute.IsDefined(type, typeof (NamedEffectAttribute))) continue;


            var attributes = type.GetCustomAttributes(typeof (NamedEffectAttribute), false);

            foreach (object attr in attributes) {
                NamedEffectAttribute effectAttribute = attr as NamedEffectAttribute;

                if (effectAttribute != null) {
                    Debug.Log(string.Format("Loaded effect {0} and stored it as {1}", type.FullName,
                        effectAttribute.EffectID));

                    // Store it
                    effectsMap[effectAttribute.EffectID] = type;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Returns a new instance of the effect searched for
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    public static IEffect GetEffect(string identifier)
    {
        // Return a new instance of the effect
        Type effectType = effectsMap[identifier];
        IEffect newEffect = (IEffect)Activator.CreateInstance(effectType);

        return newEffect;
    }

    public static CardData LoadCardFromString(string id)
    {
        CardData card = Resources.Load<CardData>(CardPath + id);

        return card;
    }
}