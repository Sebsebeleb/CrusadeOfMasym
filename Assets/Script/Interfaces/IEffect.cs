using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface IEffect
{
    // Called when this specific effect is removed. 
    void Removed();

    // Called when the effect is attached to a creature
    void SetOwner(GameObject owner);

    // Called to register to events
    void InitCallbacks();
}

public interface ISpellEffect : IEffect
{
    void OnUseCard(Owner caster, MapPosition target);
}