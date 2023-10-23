using Unity.Entities;
using UnityEngine;

namespace AuthoringAndMono
{
    public class ExecuteAuthoring : MonoBehaviour
    {
        public bool CharacterController;
    }
    class Baker : Baker<ExecuteAuthoring>
    {
        public override void Bake(ExecuteAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            //if (authoring.CharacterController) AddComponent<>(entity);
    
        }
    }
    public struct FirstPersonController : IComponentData
    {
    }
}