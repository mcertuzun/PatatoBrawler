using Unity.Entities;
using Unity.Mathematics;

namespace ComponentAndTags
{
    public struct SpawnerProperties : IComponentData
    {
        public float2 FieldDimensions;
        public int NumberOfSpawners;
        public Entity SpawnerPrefab;
        public Entity EnemyPrefab;
        public float EnemySpawnRate;
        public float3 offset;
    }
}