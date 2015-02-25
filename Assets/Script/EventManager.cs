using System;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;

public static class EventManager
{
    public static event PermanentMovedHandler OnPermanentMoved;
    public static event PermanentDestroyedHandler OnPermanentDestroyed;
    public static event CreatureAttackedHandler OnCreatureAttacked;
    public static event CreatureStartMovement OnCreatureStartMovement;
    public static event CreatureSpawned OnCreatureSpawned;

    public delegate void PermanentMovedHandler(CreatureStats mover, MapPosition from, MapPosition to);

    public delegate void PermanentDestroyedHandler(CreatureStats creature);

    public delegate void CreatureAttackedHandler(CreatureStats creature, CreatureStats source);

    public delegate void CreatureStartMovement(CreatureStats creature);

    public delegate void CreatureSpawned(CreatureStats creature, MapPosition at);


    public static void InvokePermanentMoved(CreatureStats mover, MapPosition from, MapPosition to)
    {
        var handler = OnPermanentMoved;
        if (handler != null) handler(mover, from, to);
    }

    public static void InvokePermanentDestroyed(CreatureStats creature)
    {
        var handler = OnPermanentDestroyed;
        if (handler != null) handler(creature);
    }

    public static void InvokeCreatureAttacked(CreatureStats creature, CreatureStats source)
    {
        var handler = OnCreatureAttacked;
        if (handler != null) handler(creature, source);
    }

    public static void InvokeCreatureStartMovement(CreatureStats creature)
    {
        var handler = OnCreatureStartMovement;
        if (handler != null) handler(creature);
    }

    public static void InvokeCreatureSpawned(CreatureStats creature, MapPosition at)
    {
        var handler = OnCreatureSpawned;
        if (handler != null) handler(creature, at);
    }
}
