using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.Data.Effects.TemporaryEffects;

namespace Assets.Script.Data.Effects.Spells
{
    [NamedEffect("March")]
    class March : ISpellEffect
    {

        public void OnUseCard(Owner caster, MapPosition target)
        {
            foreach (CreatureStats creature in CombatManager.GetAllCreatures()) {
                if (creature.OwnedBy == caster) {
                    MarchBuff buff = new MarchBuff();
                    creature.GetComponent<EffectHolder>().AddEffect(buff);
                }
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
