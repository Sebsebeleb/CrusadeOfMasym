
namespace Effects.CreatureEffects
{
    [NamedEffect("rogueeffect")]
    public class RogueEffect : IEffect
    {
        private CreatureStats ownerStats;

        public void Removed()
        {
        }

        public void SetOwner(UnityEngine.GameObject owner)
        {
            ownerStats = owner.GetComponent<CreatureStats>();
        }

        public void InitCallbacks()
        {
            EventManager.OnCreatureSpawned += HandleSpawned;
        }

        private void HandleSpawned(CreatureStats creature, MapPosition at)
        {
            if (creature != ownerStats)
            {
                return;
            }

            switch (ownerStats.OwnedBy)
            {
                case Owner.PLAYER:
                    ownerStats.AddAttackDirection(Direction.DOWNLEFT);
                    ownerStats.AddAttackDirection(Direction.UPLEFT);
                    break;
                case Owner.ENEMY:
                    ownerStats.AddAttackDirection(Direction.DOWNRIGHT);
                    ownerStats.AddAttackDirection(Direction.UPRIGHT);
                    break;
            }
        }
    }
}