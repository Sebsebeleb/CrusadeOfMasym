
namespace Assets.Script.Data.Effects.Spells
{
    [NamedEffect("HolyBolt")]
    internal class HolyBolt : ISpellEffect
    {
        public void Removed()
        {
        }

        public void SetOwner(UnityEngine.GameObject owner)
        {
        }

        public void InitCallbacks()
        {
        }

        public void OnUseCard(Owner caster, MapPosition target)
        {
            CreatureStats targetToHit = null;

            int start;
            int end;
            int sign;

            if (caster == Owner.PLAYER)
            {
                start = 0;
                end = Utils.IsLongLane(target) ? 15 : 14;
                sign = 1;
            }
            else
            {
                start = Utils.IsLongLane(target) ? 15 : 14;
                end = 0;
                sign = -1;
            }


            for (int i = start; i != end; i += sign)
            {
                CreatureStats hitTest = CombatManager.GetCreatureAt(new MapPosition(i, target.y));

                if (hitTest && hitTest.OwnedBy != caster)
                {
                    targetToHit = hitTest;
                    break;
                }
            }

            if (targetToHit != null)
            {
                targetToHit.TakeDamage(new Source(null, null), new Damage(5, DamageType.Spell));
            }
        }
    }
}