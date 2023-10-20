using System.Runtime.InteropServices;
using Aspects;
using ComponentAndTags;
using Helpers;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Systems
{
    [BurstCompile]
    public partial struct SpawnEnemyJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer ECB;

        private void Execute(SpawnerAspect spawner)
        {
            spawner.EnemySpawnTimer -= DeltaTime;
            if (!spawner.TimeToSpawnZombie) return;
            spawner.EnemySpawnTimer = spawner.EnemySpawnRate;
            var newEnemy = ECB.Instantiate(spawner.EnemyPrefab);

            var newEnemyTransform = spawner.GetEnemySpawnPoint();
            ECB.SetComponent(newEnemy, newEnemyTransform);

            var heading = MathHelpers.GetHeading(newEnemyTransform.Position, spawner.Position);
            ECB.SetComponent(newEnemy, new EnemyHeading() { Value = heading });
        }
    }
}