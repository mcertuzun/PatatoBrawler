using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Aspects.Player
{
    public readonly partial struct PlayerMoveAspect : IAspect
    {
        public readonly Entity Entity;
        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRO<InputProperties> _inputProperties;
        private readonly RefRO<PlayerMoveProperties> _playerMoveProperties;
        public float playerSpeed => _playerMoveProperties.ValueRO.speed;
        public float3 inputDelta =>  -math.float3(_inputProperties.ValueRO.Direction.x, 0, _inputProperties.ValueRO.Direction.y);
        
        public void Move(float deltaTime)
        {
            if(IsMove())
                _transform.ValueRW.Position += deltaTime * playerSpeed * inputDelta;
        }

        private bool IsMove()
        {
            return _inputProperties.ValueRO.OnHeld && math.length(inputDelta) >= 0.2f;
        }
    }

    public struct PlayerMoveProperties : IComponentData
    {
        public float speed;
    }
  
}