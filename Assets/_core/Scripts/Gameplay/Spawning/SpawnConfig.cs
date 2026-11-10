using System;
using UnityEngine;
using ZooWorld.Animals;

namespace ZooWorld.Spawning
{
    [CreateAssetMenu(menuName = "ZooWorld/Spawning/Spawn", fileName = "SpawnConfig")]
    public sealed class SpawnConfig : ScriptableObject
    {
        [System.Serializable]
        public struct AnimalSpawnConfig
        {
            public AnimalConfig config;
            public int maxOnScene;
        }

        [Header("Spawn Delay")]
        [SerializeField, Min(0.1f)] private float _minInterval = 1f;
        [SerializeField, Min(0.1f)] private float _maxInterval = 2f;

        [Header("Animals")]
        [SerializeField] private AnimalSpawnConfig[] _animals = Array.Empty<AnimalSpawnConfig>();

        public float MinInterval => _minInterval;
        public float MaxInterval => _maxInterval;
        public AnimalSpawnConfig[] Animals => _animals;

        private void OnValidate()
        {
            _maxInterval = Mathf.Max(_maxInterval, _minInterval);
        }
    }
}
