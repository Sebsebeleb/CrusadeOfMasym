using UnityEngine;

public class ZombieKingDebuff : TemporaryEffect
{
    private CreatureStats owner;
    private bool stoleAttack;

    private int DurationLeft;
    private const int Duration = 3; // Amount of turns to persist

    public ZombieKingDebuff(bool stealAttack)
    {
        stoleAttack = stealAttack;

        DurationLeft = Duration;
    }

    public override void Removed()
    {
        if (stoleAttack)
        {
            owner.Attack++;
        }
        owner.MaxHealth++;
        owner.SetHealth(owner.GetHealth() + 1);
    }

    public override void SetOwner(GameObject newOwner)
    {
        owner = newOwner.GetComponent<CreatureStats>();
        if (stoleAttack)
        {
            owner.Attack--;
        }
        owner.SetHealth(owner.GetHealth() - 1);
        owner.MaxHealth--;
    }

    protected override void UnregisterCallbacks()
    {
        EventManager.OnCreatureStartMovement -= OnCreatureStartMovement;
    }

    public override void InitCallbacks()
    {
        EventManager.OnCreatureStartMovement += OnCreatureStartMovement;
    }

    private void OnCreatureStartMovement(CreatureStats creature)
    {
        if (creature == owner)
        {
            DoTurn();
        }
    }

    private void DoTurn()
    {
        DurationLeft--;
        if (DurationLeft <= 0)
        {
            owner.GetComponent<EffectHolder>().RemoveEffect(this);
        }
    }
}