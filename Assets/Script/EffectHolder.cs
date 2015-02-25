﻿using System.Collections.Generic;
using UnityEngine;
using System.Collections;

/// <summary>
/// Container for effects currently in play in this object.
/// </summary>
public class EffectHolder : MonoBehaviour
{

    // For unity, each string added here will be initalized and added on startup, found using DataLibrary 
    public string[] StartEffects; 

    private List<IEffect> Effects = new List<IEffect>(); 

    void Start()
    {
        foreach (string eff in StartEffects) {
            IEffect effect = DataLibrary.GetEffect(eff);
            AddEffect(effect);
        }
    }

    void Update()
    {

    }

    void AddEffect(IEffect effect)
    {
        effect.SetOwner(gameObject);
        effect.InitCallbacks();

        Effects.Add(effect);
    }

    void RemoveAllEffects()
    {
        foreach (IEffect effect in Effects) {
            effect.Removed();
        }

        Effects.Clear();
    }

    void OnDestroy()
    {
        Debug.Log("Uh hello?");
        foreach (IEffect effect in Effects) {
            effect.Removed();
        }
    }
}