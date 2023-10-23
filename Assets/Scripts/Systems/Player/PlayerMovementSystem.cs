using Aspects.Player;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems.Move
{
    [UpdateAfter(typeof(InputSystem))]
    public partial struct PlayerMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InputProperties>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var player in SystemAPI.Query<PlayerMoveAspect>())
            {
                player.Move(deltaTime);
            }
        }

    }
}