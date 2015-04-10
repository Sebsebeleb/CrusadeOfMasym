using System.Collections.Generic;

namespace Assets.Script.Data.Effects.Spells
{
    [NamedEffect("HolyLight")]
    internal class HolyLight : ISpellEffect
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
            foreach (MapPosition position in Utils.GetAdjacent(target))
            {
                positions.Add(position);
            }

            foreach (MapPosition pos in positions)
            {
                CreatureStats creatureAt = CombatManager.GetCreatureAt(pos);
                if (creatureAt && creatureAt.OwnedBy == caster)
                {
                    creatureAt.Heal(3);
                }
            }
        }
    }
}