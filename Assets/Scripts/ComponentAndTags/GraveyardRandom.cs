using Unity.Entities;
using Unity.Mathematics;

namespace ComponentAndTags
{
    public struct GraveyardRandom : IComponentData
    {
        public Random Value;
    }
}