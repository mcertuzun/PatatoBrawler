
using Unity.Entities;
using Unity.Mathematics;

namespace ComponentAndTags
{
    public struct GraveyardProperties : IComponentData
    {
        public float2 FieldDimensions;
        public int NumberTombstonesToSpawn;
        public Entity TombstonePrefab;
        public float3 offset;
    }
}
