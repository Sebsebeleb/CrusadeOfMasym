using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Data.Effects.TemporaryEffects
{
    class SleepDebuff : TemporaryEffect
    {
        private CreatureStats ownerStats;
        private int _duration;

        public SleepDebuff(int duration)
        {
            _duration = duration;
        }
        public override void InitCallbacks()
        {
            base.InitCallbacks();
            EventManager.OnEndOfTurn += OnTurn;
        }

        protected override void UnregisterCallbacks()
        {
            base.UnregisterCallbacks();
            EventManager.OnEndOfTurn -= OnTurn;
        }

        public override void SetOwner(GameObject owner)
        {
            base.SetOwner(owner);

            ownerStats = owner.GetComponent<CreatureStats>();
            ownerStats.SetCanAct(false);
        }

        private  void OnTurn()
        {

            _duration--;
            if (_duration <= 0) {
                ownerStats.GetComponent<EffectHolder>().RemoveEffect(this);
            }
        }

        public override void Removed()
        {
            base.Removed();

            ownerStats.SetCanAct(true);
        }
    }
}
