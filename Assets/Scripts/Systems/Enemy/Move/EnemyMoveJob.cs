using System.Runtime.InteropServices;
using Aspects;
using Unity.Burst;
using Unity.Entities;

namespace Systems.Move
{
    [BurstCompile]
    public partial struct EnemyMoveJob : IJobEntity
    {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(EnemyMoveAspect enemy)
        {
            enemy.Move(DeltaTime);
        }
    }
}