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
    public partial struct SpawnTombstoneSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GraveyardProperties>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            var graveyardEntity = SystemAPI.GetSingletonEntity<GraveyardProperties>();
            var graveyard = SystemAPI.GetAspect<GraveyardAspect>(graveyardEntity);
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            var builder = new BlobBuilder(Allocator.Temp);
            ref var spawnPoints = ref builder.ConstructRoot<ZombieSpawnPointsBlob>();
            var arrayBuilder = builder.Allocate(ref spawnPoints.Value, graveyard.NumberTombstonesToSpawn);
            
            for (var i = 0; i < graveyard.NumberTombstonesToSpawn; i++)
            {
                var newEntity = ecb.Instantiate(graveyard.TombStonePrefab);
                var tempTombstoneTransform = graveyard.GetRandomTombstoneTransform();
                var newZombieSpawnPoint = tempTombstoneTransform.Position + graveyard.GetTombstoneOffset();
                arrayBuilder[i] = newZombieSpawnPoint;
                ecb.SetComponent(newEntity, tempTombstoneTransform);
            }
            var blobAsset = builder.CreateBlobAssetReference<ZombieSpawnPointsBlob>(Allocator.Persistent);
            ecb.SetComponent(graveyardEntity, new ZombieSpawnPoints{Value = blobAsset});
            builder.Dispose();
            ecb.Playback(state.EntityManager);
        }
    }
}