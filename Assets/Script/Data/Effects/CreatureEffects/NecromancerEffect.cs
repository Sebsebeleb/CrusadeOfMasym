using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Effects.CreatureEffects
{
    [NamedEffect("necromancereffect")]
    public class NecromancerEffect : IEffect
    {
        private CreatureStats ownerStats;

        private List<MapPosition> spawnPositions = new List<MapPosition>();

        public void Removed()
        {
        }

        public void SetOwner(UnityEngine.GameObject owner)
        {
            ownerStats = owner.GetComponent<CreatureStats>();
        }

        public void InitCallbacks()
        {
            EventManager.OnPermanentDestroyed += HandleDeath;
            EventManager.OnEndOfTurn += HandleSpawning;
        }

        private void HandleSpawning()
        {
            foreach (MapPosition position in spawnPositions) {
                SpawnZombie(position);
            }
            spawnPositions.Clear();
        }

        private void SpawnZombie(MapPosition position)
        {
            GameObject zombiePrefab = DataLibrary.GetCreatureFromName("Zombie Husk");

            if (CombatManager.CanSpawn(zombiePrefab, TurnManager.CurrentPlayer, position)) {
                CombatManager.SpawnPermanent(zombiePrefab, TurnManager.CurrentPlayer, position);
            }
        }

        private void HandleDeath(CreatureStats creature)
        {
            bool isAdjacent = Utils.IsAdjacent(ownerStats.GridPosition, creature.GridPosition);
            // If the creature is adjacent, and it's name doesnt start with zombie
            if (Utils.IsAdjacent(ownerStats.GridPosition, creature.GridPosition)
                && !creature.name.ToLower().StartsWith("zombie")) {
                RegisterDeath(creature.GridPosition);
            }
        }

        private void RegisterDeath(MapPosition pos)
        {
            spawnPositions.Add(pos);
        }
    }
}