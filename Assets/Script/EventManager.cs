using Assets.Script;

public static class EventManager
{
    public static event PermanentMovedHandler OnPermanentMoved;
    public static event PermanentDestroyedHandler OnPermanentDestroyed;
    public static event CreatureAttackedHandler OnCreatureAttacked; // When a creature is attacked. passes the attacked creature and attack info
    public static event CreatureStartMovement OnCreatureStartMovement;
    public static event CreatureSpawned OnCreatureSpawned;
    public static event EndOfTurn OnEndOfTurn;
    public static event CreatureAttack OnCreatureAttack; // When a creature attacks, passes the attacking creature and attack info

    public delegate void PermanentMovedHandler(CreatureStats mover, MapPosition from, MapPosition to);

    public delegate void PermanentDestroyedHandler(CreatureStats creature, Source killSource);

    public delegate void CreatureAttackedHandler(CreatureStats creature, CreatureStats source);

    public delegate void CreatureStartMovement(CreatureStats creature);

    public delegate void CreatureSpawned(CreatureStats creature, MapPosition at);

    public delegate void EndOfTurn();

    public delegate void CreatureAttack(CreatureStats attacker, CreatureStats target, Damage damageDone);


    public static void InvokePermanentMoved(CreatureStats mover, MapPosition from, MapPosition to)
    {
        var handler = OnPermanentMoved;
        if (handler != null) handler(mover, from, to);
    }

    public static void InvokePermanentDestroyed(CreatureStats creature, Source killSource)
    {
        var handler = OnPermanentDestroyed;
        if (handler != null) handler(creature, killSource);
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

    public static void InvokeEndOfTurn()
    {
        var handler = OnEndOfTurn;
        if (handler != null) handler();
    }

    public static void InvokeCreatureAttack(CreatureStats creature, CreatureStats target, Damage damageDone)
    {
        var handler = OnCreatureAttack;
        if (handler != null) handler(creature, target, damageDone);
    }
}
