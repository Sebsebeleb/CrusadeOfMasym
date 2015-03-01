using UnityEngine;
using System.Collections;

namespace Effects
{
    [NamedEffect("DivineLight")]
    public class DivineLight : ISpellEffect
    {
        public void OnUseCard(Owner caster, MapPosition target)
        {
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