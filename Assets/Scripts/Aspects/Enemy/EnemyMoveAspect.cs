using ComponentAndTags;
using Helpers;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Aspects
{
    public readonly partial struct EnemyMoveAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRW<Timer> _timer;
        private readonly RefRO<EnemyMoveProperties> _enemyMoveProperties;
        private readonly RefRO<EnemyHeading> _heading;

        private float MoveSpeed => _enemyMoveProperties.ValueRO.Speed;
        private float MoveAmplitude => _enemyMoveProperties.ValueRO.Amplitude;
        private float MoveFrequency => _enemyMoveProperties.ValueRO.Frequency;
        private float Heading => _heading.ValueRO.Value;

        private float Timer
        {
            get => _timer.ValueRO.Value;
            set => _timer.ValueRW.Value = value;
        }

        public void Move(float deltaTime)
        {
            Timer += deltaTime;
            _transform.ValueRW.Position += _transform.ValueRO.Forward() * MoveSpeed * deltaTime;
            _transform.ValueRW.Rotation = quaternion.Euler(0, Heading, 0);
        }
    }
}