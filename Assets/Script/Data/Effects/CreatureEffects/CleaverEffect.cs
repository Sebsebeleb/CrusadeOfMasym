﻿using Assets.Script;
using UnityEngine;

namespace Effects.CreatureEffects
{
    [NamedEffect("cleavereffect")]
    public class CleaverEffect : IEffect
    {
        private CreatureStats ownerStats;

        public void Removed()
        {
        }

        public void SetOwner(GameObject owner)
        {
            ownerStats = owner.GetComponent<CreatureStats>();
        }

        public void InitCallbacks()
        {
            EventManager.OnCreatureAttack += HandleAttack;
        }
        
        private void HandleAttack(CreatureStats attacker, CreatureStats target, Damage damagedone)
        {

            Direction directionUp;
            Direction directionDown;

            if (attacker.OwnedBy == Owner.PLAYER)
            {
                directionDown = Direction.DOWNRIGHT;
                directionUp = Direction.UPRIGHT;
            }
            else
            {
                directionDown = Direction.DOWNLEFT;
                directionUp = Direction.UPLEFT;
            }

            CreatureStats enemyUp = CombatManager.GetCreatureAt(attacker.GridPosition.InDirection(directionUp));
            CreatureStats enemyDown = CombatManager.GetCreatureAt(attacker.GridPosition.InDirection(directionDown));

            if (enemyDown) {
                enemyDown.TakeDamage(new Source(attacker, null), new Damage((int)Mathf.Round(damagedone.Value * 0.5f), DamageType.Physical));
            }
            if (enemyUp) {
                enemyUp.TakeDamage(new Source(attacker, null), new Damage((int)Mathf.Round(damagedone.Value * 0.5f), DamageType.Physical));
            }
        }
    }
}
