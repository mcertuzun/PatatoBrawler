using Unity.Entities;
using Unity.Mathematics;

namespace ComponentAndTags
{
    public struct SpawnerRandom : IComponentData
    {
        public Random Value;
    }
}