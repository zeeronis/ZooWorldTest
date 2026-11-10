using System.Collections.Generic;
using UnityEngine;
using ZooWorld.Infrastructure;

namespace ZooWorld.Animals
{
    public sealed class AnimalsProcessor : IGameUpdatable, IGameFixedUpdatable
    {
        private const float MinTurnSpeedSqr = 0.01f;
        private const float TurnSharpness = 10f;

        private readonly List<AnimalCtrl> _animals = new();
        private readonly Dictionary<AnimalConfig, int> _countByConfig = new();
        private readonly IGameObjectPool _pool;

        public AnimalsProcessor(IGameObjectPool pool)
        {
            _pool = pool;
        }

        public IReadOnlyList<AnimalCtrl> Animals => _animals;

        public int CountOf(AnimalConfig config)
        {
            return _countByConfig.TryGetValue(config, out var count) ? count : 0;
        }

        public void Register(AnimalCtrl animal)
        {
            _animals.Add(animal);
            _countByConfig[animal.Config] = CountOf(animal.Config) + 1;
        }

        public void Remove(AnimalCtrl animal)
        {
            if (!_animals.Remove(animal))
            {
                return;
            }

            _countByConfig[animal.Config]--;
            _pool.Release(animal.gameObject);
        }

        public void OnUpdate(float deltaTime)
        {
            for (var i = 0; i < _animals.Count; i++)
            {
                FaceVelocity(_animals[i], deltaTime);
            }
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            for (var i = 0; i < _animals.Count; i++)
            {
                _animals[i].Movement.OnFixedUpdate(fixedDeltaTime);
            }
        }

        private static void FaceVelocity(AnimalCtrl animal, float deltaTime)
        {
            var velocity = animal.Rigidbody.linearVelocity;
            velocity.y = 0f;
            if (velocity.sqrMagnitude <= MinTurnSpeedSqr)
            {
                return;
            }

            var visual = animal.Visual;
            var target = Quaternion.LookRotation(velocity);
            visual.rotation = Quaternion.Slerp(visual.rotation, target, TurnSharpness * deltaTime);
        }
    }
}
