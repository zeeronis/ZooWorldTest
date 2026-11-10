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
        private readonly AnimalLifecycle _lifecycle;
        private readonly AnimalRegistry _registry;
        private readonly IRandomService _random;
        private float _timer;

        public AnimalSpawner(Level level, AnimalLifecycle lifecycle, AnimalRegistry registry, IRandomService random)
        {
            _level = level;
            _config = level.SpawnConfig;
            _lifecycle = lifecycle;
            _registry = registry;
            _random = random;
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
                if (entry.config != null && _registry.CountOf(entry.config) < entry.maxOnScene)
                {
                    _candidates.Add(entry.config);
                }
            }

            if (_candidates.Count == 0)
            {
                return false;
            }

            _lifecycle.Spawn(_candidates[_random.Range(0, _candidates.Count)], FindSpawnPoint());
            return true;
        }

        private Vector3 FindSpawnPoint()
        {
            var point = _level.Center;
            for (var i = 0; i < PointAttempts; i++)
            {
                point = _level.RandomPoint(EdgePadding, _random);
                if (!Physics.CheckSphere(point + Vector3.up * CheckHeight, FreeRadius, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
                {
                    break;
                }
            }

            return point;
        }

        private float NextInterval()
        {
            return _random.Range(_config.MinInterval, _config.MaxInterval);
        }
    }
}
