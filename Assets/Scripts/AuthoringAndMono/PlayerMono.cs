using Aspects.Player;
using ComponentAndTags;
using Unity.Entities;
using UnityEngine;

namespace AuthoringAndMono
{
    public class PlayerMono : MonoBehaviour
    {
        public float speed;
    }

    public class PlayerBaker : Baker<PlayerMono>
    {
        public override void Bake(PlayerMono authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity,new PlayerMoveProperties()
            {
                speed = authoring.speed,
            });
            AddComponent<InputProperties>(entity);
        }
    }
}