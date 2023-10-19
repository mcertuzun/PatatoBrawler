using Aspects;
using ComponentAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct GenerateSpawnerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SpawnerProperties>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            var spawnerEntity = SystemAPI.GetSingletonEntity<SpawnerProperties>();
            var spawner = SystemAPI.GetAspect<SpawnerAspect>(spawnerEntity);
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            var builder = new BlobBuilder(Allocator.Temp);
            ref var spawnPoints = ref builder.ConstructRoot<EnemySpawnPointsBlob>();
            var arrayBuilder = builder.Allocate(ref spawnPoints.Value, spawner.NumberOfSpawners);
            
            for (var i = 0; i < spawner.NumberOfSpawners; i++)
            {
                var newEntity = ecb.Instantiate(spawner.SpawnerPrefab);
                var tempSpawnerTransform = spawner.GetRandomSpawnerTransform();
                var newEnemySpawnPoint = tempSpawnerTransform.Position + spawner.GetEnemyOffset();
                arrayBuilder[i] = newEnemySpawnPoint;
                ecb.SetComponent(newEntity, tempSpawnerTransform);
            }
            var blobAsset = builder.CreateBlobAssetReference<EnemySpawnPointsBlob>(Allocator.Persistent);
            ecb.SetComponent(spawnerEntity, new SpawnerLocations{Value = blobAsset});
            builder.Dispose();
            ecb.Playback(state.EntityManager);
        }
    }
}