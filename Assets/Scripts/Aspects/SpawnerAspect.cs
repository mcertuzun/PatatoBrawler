using ComponentAndTags;
using Helpers;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Aspects
{
    public readonly partial struct SpawnerAspect : IAspect
    {
        public readonly Entity Entity;
        private readonly RefRO<LocalTransform> _transform;
        private LocalTransform Transform => _transform.ValueRO;

        private readonly RefRO<SpawnerProperties> _spawnPointProperties;
        private readonly RefRW<SpawnerRandom> _spawnPointRandom;
        private readonly RefRW<SpawnerLocations> _spawnPointLocations;
        private readonly RefRW<Timer> _enemySpawnTimer;
        public int NumberOfSpawners => _spawnPointProperties.ValueRO.NumberOfSpawners;
        public Entity SpawnerPrefab => _spawnPointProperties.ValueRO.SpawnerPrefab;
        public Entity PlayerPrefab => _spawnPointProperties.ValueRO.PlayerPrefab;

        private int EnemySpawnPointCount => _spawnPointLocations.ValueRO.Value.Value.Value.Length;

        public LocalTransform GetRandomSpawnerTransform()
        {
            return new LocalTransform()
            {
                Position = GetRandomSpawnerPosition(),
                Rotation = GetRandomSpawnerRotation(),
                Scale = GetRandomSpawnerScale(0.5f)
            };
        }
        private float3 GetRandomSpawnerPosition()
        {
            float3 randomPosition;
            do
            {
                randomPosition = _spawnPointRandom.ValueRW.Value.NextFloat3(MinCorner, MaxCorner);
            } while (math.distancesq(_transform.ValueRO.Position, randomPosition) <= PLAYER_SAFETY_RADIUS - 1);

            return randomPosition;
        }
        private quaternion GetRandomSpawnerRotation() => quaternion.RotateY(_spawnPointRandom.ValueRW.Value.NextFloat(-0.25f, 0.25f));
        private float GetRandomSpawnerScale(float min) => _spawnPointRandom.ValueRW.Value.NextFloat(min, 1f);
        private float3 MinCorner => Transform.Position - HalfDimensions;

        private float3 MaxCorner => Transform.Position + HalfDimensions;

        private float3 HalfDimensions => new()
        {
            x = _spawnPointProperties.ValueRO.FieldDimensions.x * 0.5f,
            y = 0,
            z = _spawnPointProperties.ValueRO.FieldDimensions.y * 0.5f
        };

        public float3 Position => Transform.Position;


        public float3 GetEnemyOffset()
        {
            return _spawnPointProperties.ValueRO.offset;
        }

        public float EnemySpawnTimer
        {
            get => _enemySpawnTimer.ValueRO.Value;
            set => _enemySpawnTimer.ValueRW.Value = value;
        }

        public bool TimeToSpawnZombie => EnemySpawnTimer <= 0f;
        public float EnemySpawnRate => _spawnPointProperties.ValueRO.EnemySpawnRate;
        public Entity EnemyPrefab => _spawnPointProperties.ValueRO.EnemyPrefab;

        public LocalTransform GetEnemySpawnPoint()
        {
            var position = GetRandomEnemySpawnPoint();
            return new LocalTransform
            {
                Position = position,
                Rotation = quaternion.RotateY(MathHelpers.GetHeading(position, _transform.ValueRO.Position)),
                Scale = 1f
            };
        }

        private float3 GetRandomEnemySpawnPoint()
        {
            return GetEnemySpawnPoint(_spawnPointRandom.ValueRW.Value.NextInt(EnemySpawnPointCount));
        }

        public bool EnemySpawnPointInitialized()
        {
            return _spawnPointLocations.ValueRO.Value.IsCreated && EnemySpawnPointCount > 0;
        }


        private float3 GetEnemySpawnPoint(int i)
        {
            return _spawnPointLocations.ValueRO.Value.Value.Value[i];
        }

        private const float PLAYER_SAFETY_RADIUS = 100;
    }
}