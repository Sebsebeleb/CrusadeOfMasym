using UnityEngine;

namespace Effects
{
    [NamedEffect("Divine Light")]
    public class DivineLight : ISpellEffect
    {
        public void OnUseCard(Owner caster, MapPosition target)
        {
            CreatureStats creature = CombatManager.GetCreatureAt(target);

            if (creature != null)
            {
                creature.Heal(5);
            }
        }

        public void Removed()
        {
        }

        public void SetOwner(GameObject owner)
        {
        }

        public void InitCallbacks()
        {
        }
    }
}