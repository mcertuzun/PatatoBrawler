using ComponentAndTags;
using Unity.Entities;
using UnityEngine;

namespace AuthoringAndMono
{
    public class EnemyMono : MonoBehaviour
    {
        public float RiseRate;
        public float moveSpeed;
        public float moveAmplitude;
        public float moveFrequency;
    }

    public class ZombieBaker : Baker<EnemyMono>
    {
        public override void Bake(EnemyMono authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new EnemyRiseRate
            {
                Value = authoring.RiseRate
            });
            AddComponent(entity, new EnemyMoveProperties
            {
                Speed = authoring.moveSpeed,
                Amplitude = authoring.moveAmplitude,
                Frequency = authoring.moveFrequency
            });
            AddComponent<NewEnemyTag>(entity);
            AddComponent<Timer>(entity);
            AddComponent<EnemyHeading>(entity);
        }
    }
}