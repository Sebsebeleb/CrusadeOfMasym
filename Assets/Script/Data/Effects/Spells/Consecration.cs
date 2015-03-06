using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Data.Effects.Spells
{
    [NamedEffect("Consecration")]
    internal class Consecration : ISpellEffect
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
            List<MapPosition> positions = new List<MapPosition>();
            positions.Add(target);
            foreach (MapPosition position in Utils.GetAdjacent(target)) {
                positions.Add(position);
            }

            foreach (MapPosition pos in positions) {
                CreatureStats creatureAt = CombatManager.GetCreatureAt(pos);
                if (creatureAt) {
                    creatureAt.TakeDamage(new Source(), new Damage(2, DamageType.Spell));
                }
            }
        }
    }
}