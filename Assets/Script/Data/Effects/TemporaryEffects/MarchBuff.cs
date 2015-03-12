using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Data.Effects.TemporaryEffects
{
    class MarchBuff : TemporaryEffect
    {
        private CreatureStats ownerStats;
        private int DurationLeft;

        public override void Removed()
        {
            base.Removed();

            ownerStats.Speed -= 1;
            ownerStats.Attack -= 1;
        }

        public override void SetOwner(UnityEngine.GameObject owner)
        {
            ownerStats = owner.GetComponent<CreatureStats>();
            ownerStats.Speed += 1;
            ownerStats.Attack += 1;

            DurationLeft = 3;

        }

        public override void InitCallbacks()
        {
            EventManager.OnCreatureStartMovement += OnTurn;
        }

        protected override void UnregisterCallbacks()
        {
            EventManager.OnCreatureStartMovement -= OnTurn;
        }

        private void OnTurn(CreatureStats creature)
        {
            if (creature != ownerStats) {
                return;
            }

            DurationLeft--;
            if (DurationLeft <= 0) {
                ownerStats.GetComponent<EffectHolder>().RemoveEffect(this);
            }
        }
    }
}
