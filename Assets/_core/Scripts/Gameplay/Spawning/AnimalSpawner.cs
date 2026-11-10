using System.Collections.Generic;
using UnityEngine;
using ZooWorld.Animals;
using ZooWorld.Infrastructure;
using ZooWorld.Levels;

namespace ZooWorld.Spawning
{
    public sealed class AnimalSpawner : IGameUpdatable
    {
        private const float EdgePadding = 1f;
        private const float FreeRadius = 1f;
        private const float CheckHeight = 0.5f;
        private const int PointAttempts = 5;

        private readonly List<AnimalConfig> _candidates = new();
        private readonly Level _level;
        private readonly SpawnConfig _config;
        private readonly AnimalFactory _factory;
        private readonly AnimalsProcessor _animals;
        private float _timer;

        public AnimalSpawner(LevelBuilder levelBuilder, AnimalFactory factory, AnimalsProcessor animals)
        {
            _level = levelBuilder.Level;
            _config = _level.SpawnConfig;
            _factory = factory;
            _animals = animals;
            _timer = NextInterval();
        }

        public void OnUpdate(float deltaTime)
        {
            _timer -= deltaTime;
            if (_timer > 0f || !TrySpawn())
            {
                return;
            }

            _timer = NextInterval();
        }

        private bool TrySpawn()
        {
            _candidates.Clear();
            foreach (var entry in _config.Animals)
            {
                if (entry.config != null && _animals.CountOf(entry.config) < entry.maxOnScene)
                {
                    _candidates.Add(entry.config);
                }
            }

            if (_candidates.Count == 0)
            {
                return false;
            }

            _factory.Create(_candidates[Random.Range(0, _candidates.Count)], FindSpawnPoint());
            return true;
        }

        private Vector3 FindSpawnPoint()
        {
            var center = _level.transform.position;
            var half = _level.ArenaSize * 0.5f - Vector2.one * EdgePadding;
            var point = center;

            for (var i = 0; i < PointAttempts; i++)
            {
                point = center + new Vector3(Random.Range(-half.x, half.x), 0f, Random.Range(-half.y, half.y));
                if (!Physics.CheckSphere(point + Vector3.up * CheckHeight, FreeRadius, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
                {
                    break;
                }
            }

            return point;
        }

        private float NextInterval()
        {
            return Random.Range(_config.MinInterval, _config.MaxInterval);
        }
    }
}
