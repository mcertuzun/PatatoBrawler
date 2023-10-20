using Aspects;
using ComponentAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateAfter(typeof(GenerateSpawnerSystem))]
    public partial struct InitializeEnemySystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var enemy in SystemAPI.Query<EnemyMoveAspect>().WithAll<NewEnemyTag>())
            {
                ecb.RemoveComponent<NewEnemyTag>(enemy.Entity);
                ecb.SetComponentEnabled<EnemyMoveProperties>(enemy.Entity, false);
            }

            ecb.Playback(state.EntityManager);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}