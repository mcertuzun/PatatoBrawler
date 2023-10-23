using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Systems.Move
{
    public partial struct InputSystem : ISystem
    {
        private float3 originDelta, origin;

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InputProperties>();
        }

        public void OnUpdate(ref SystemState state)
        {
            ref var input = ref SystemAPI.GetSingletonRW<InputProperties>().ValueRW;
            if (Input.GetMouseButtonDown(0))
            {
                input.OnRelease = false;
                origin = Input.mousePosition;
                originDelta = Input.mousePosition;
                input.OnPress = true;
            }

            if (Input.GetMouseButton(0))
            {
                input.OnPress = false;
                float3 currentPosition = Input.mousePosition;
                input.Delta = math.normalize(originDelta - currentPosition);
                input.Direction = math.normalize(origin - currentPosition);
                input.OnHeld = true;
            }

            originDelta = Input.mousePosition;;
            if (Input.GetMouseButtonUp(0))
            {
                input.OnHeld = false;
                input.OnRelease = true;
                input.Delta = float3.zero;
                input.Direction = float3.zero;
                input.OnRelease = false;
            }
        }
    }
   
}

public struct InputProperties : IComponentData
{
    public float3 Direction;
    public float3 Delta;
    public bool OnPress;
    public bool OnHeld;
    public bool OnRelease;
}