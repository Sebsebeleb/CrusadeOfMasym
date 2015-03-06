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
