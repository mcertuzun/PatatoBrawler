using Unity.Burst;
using Unity.Entities;

namespace Systems.Move
{
    [BurstCompile]
    [UpdateAfter(typeof(EnemyRiseSystem))]
    public partial struct EnemyMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            new EnemyMoveJob()
            {
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }
    }
}