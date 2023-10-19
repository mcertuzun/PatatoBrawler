using ComponentAndTags;
using Unity.Entities;
using UnityEngine;

namespace AuthoringAndMono
{
    public class EnemyMono : MonoBehaviour
    {
        public float RiseRate;
    }

    public class ZombieBaker : Baker<EnemyMono>
    {
        
        public override void Bake(EnemyMono authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity,new EnemyRiseRate
            {
                Value = authoring.RiseRate,
            });
        }
    }
}

