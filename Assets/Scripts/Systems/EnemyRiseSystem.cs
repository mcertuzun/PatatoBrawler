using System.Runtime.InteropServices;
using Aspects;
using ComponentAndTags;
using Unity.Burst;
using Unity.Entities;

namespace Systems
{
    [BurstCompile]
    [UpdateAfter(typeof(SpawnEnemySystem))]
    public partial struct EnemyRiseSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            new EnemyRiseJob
            {
                DeltaTime = deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct EnemyRiseJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;

        [BurstCompile]
        private void Execute(EnemyRiseAspect enemy, [ChunkIndexInQuery]int sortKey)
        {
            enemy.Rise(DeltaTime);
            if(!enemy.IsAboveGround)return;
            enemy.SetAtGroundLevel();
            ECB.RemoveComponent<EnemyRiseRate>(sortKey, enemy.Entity);
        }
    }
}