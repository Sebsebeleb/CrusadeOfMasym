using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.Data.Effects.TemporaryEffects;

namespace Assets.Script.Data.Effects.Spells
{
    [NamedEffect("Sleep")]
    class Sleep : ISpellEffect
    {
        public void OnUseCard(Owner caster, MapPosition target)
        {
            CreatureStats creature = CombatManager.GetCreatureAt(target);

            if (creature != null) {
                SleepDebuff debuff = new SleepDebuff(3);
                creature.GetComponent<EffectHolder>().AddEffect(debuff);
            }
        }

        public void Removed()
        {
        }

        public void SetOwner(UnityEngine.GameObject owner)
        {
        }

        public void InitCallbacks()
        {
        }
    }
}
