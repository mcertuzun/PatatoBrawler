using Unity.Entities;

namespace ComponentAndTags
{
    public struct EnemyMoveProperties : IComponentData, IEnableableComponent
    {
        public float Speed;
        public float Amplitude;
        public float Frequency;
    }

    public struct NewEnemyTag : IComponentData
    {
    }
}