﻿using ComponentAndTags;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = Unity.Mathematics.Random;

namespace AuthoringAndMono
{
    public class SpawnerMono : MonoBehaviour
    {
        public float2 FieldDimensions;
        public int NumberOfSpawners;
        public GameObject SpawnerPrefab;
        public GameObject EnemyPrefab;
        public float EnemySpawnRate;
        public uint RandomSeed;
        public float3 EnemyOffset;
    }

    public class SpawnerBaker : Baker<SpawnerMono>
    {
        public override void Bake(SpawnerMono authoring)
        {
            var graveyardEntity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(graveyardEntity, new SpawnerProperties
            {
                FieldDimensions = authoring.FieldDimensions,
                NumberOfSpawners = authoring.NumberOfSpawners,
                SpawnerPrefab = GetEntity(authoring.SpawnerPrefab, TransformUsageFlags.Dynamic),
                EnemyPrefab = GetEntity(authoring.EnemyPrefab, TransformUsageFlags.Dynamic),
                EnemySpawnRate = authoring.EnemySpawnRate,
                offset = authoring.EnemyOffset,
            });
            AddComponent(graveyardEntity, new SpawnerRandom
            {
                Value = Random.CreateFromIndex(authoring.RandomSeed)
            });
            AddComponent<SpawnerLocations>(graveyardEntity);
            AddComponent<EnemySpawnTimer>(graveyardEntity);
        }
    }
}