using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script
{
    /// <summary>
    /// The source of a damage, effect, etc.
    /// </summary>
    public class Source
    {
        public CreatureStats Creature;
        public CardData Card;
        public Source(CreatureStats creature = null, CardData card = null)
        {
            Card = card;
            Creature = creature;
        }
    }
}
