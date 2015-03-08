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

public abstract class TemporaryEffect : IEffect
{
    ~TemporaryEffect()
    {
        UnregisterCallbacks();
    }

    protected virtual void UnregisterCallbacks()
    {
        
    }

    public virtual void Removed()
    {
        UnregisterCallbacks();
    }

    public virtual void SetOwner(GameObject owner)
    {
    }

    public virtual void InitCallbacks()
    {

    }
}