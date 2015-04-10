using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script
{
    public enum DamageType
    {
        Physical,
        Spell,
        True, // Special damage type from special effects, like fury dealing 2 damage to owner each turn. This damage cannot be prevented
    }

    /// <summary>
    /// Value type representing damage to something
    /// </summary>
    public class Damage
    {
        public readonly int Value;
        public readonly DamageType damageType;

        public Damage(int val, DamageType damType)
        {
            Value = val;
            damageType = damType;
        }
    }

}
