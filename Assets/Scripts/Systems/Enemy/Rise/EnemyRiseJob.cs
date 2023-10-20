using Aspects;
using ComponentAndTags;
using Unity.Burst;
using Unity.Entities;

namespace Systems
{
    [BurstCompile]
    public partial struct EnemyRiseJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;

        [BurstCompile]
        private void Execute(EnemyRiseAspect enemy, [ChunkIndexInQuery] int sortKey)
        {
            enemy.Rise(DeltaTime);
            if (!enemy.IsAboveGround) return;
            enemy.SetAtGroundLevel();
            ECB.RemoveComponent<EnemyRiseRate>(sortKey, enemy.Entity);
            ECB.SetComponentEnabled<EnemyMoveProperties>(sortKey, enemy.Entity, true);
        }
    }
}