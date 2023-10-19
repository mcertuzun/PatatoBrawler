using Aspects;
using ComponentAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    [BurstCompile]
    public partial struct SpawnEnemySystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecb = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            new SpawnEnemyJob()
            {
                DeltaTime = deltaTime,
                ECB = ecb.CreateCommandBuffer(state.WorldUnmanaged),
            }.Run();
        }
    }
    
    
    [BurstCompile]
    public partial struct SpawnEnemyJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer ECB;
        private void Execute(SpawnerAspect spawner)
        {
            spawner.EnemySpawnTimer -= DeltaTime;
            if(!spawner.TimeToSpawnZombie) return;
            spawner.EnemySpawnTimer = spawner.EnemySpawnRate;
            var newEnemy = ECB.Instantiate(spawner.EnemyPrefab);
            var newEnemyTransform = spawner.GetEnemySpawnPoint();
            ECB.SetComponent(newEnemy, newEnemyTransform);
        }
    }
    
}