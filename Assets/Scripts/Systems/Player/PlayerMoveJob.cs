using Aspects;
using Aspects.Player;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace Systems.Move
{
    [BurstCompile]
    public partial struct PlayerMoveJob : IJobEntity
    {
        public float DeltaTime;
        [BurstCompile]
        private void Execute(PlayerMoveAspect player)
        {
            player.Move(DeltaTime);
        }
    }
}