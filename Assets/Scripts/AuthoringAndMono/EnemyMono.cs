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
        }
    }
}

