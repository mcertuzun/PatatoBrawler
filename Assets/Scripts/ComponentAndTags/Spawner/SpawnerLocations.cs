using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace ComponentAndTags
{
    public struct SpawnerLocations : IComponentData
    {
        public BlobAssetReference<EnemySpawnPointsBlob> Value;
    }

    public struct EnemySpawnPointsBlob
    {
        public BlobArray<float3> Value;
    }
}