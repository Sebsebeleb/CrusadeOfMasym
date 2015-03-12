using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Data.Effects.Spells
{
    [NamedEffect("Purge")]
    class Purge : ISpellEffect
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
            CreatureStats creature = CombatManager.GetCreatureAt(target);
            if (creature != null) {
                creature.GetComponent<EffectHolder>().RemoveAllEffects();
            }
        }
    }
}
