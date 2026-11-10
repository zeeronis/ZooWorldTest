using UnityEngine;
using ZooWorld.Infrastructure;

namespace ZooWorld.Animals
{
    // Drives every animal on the scene: movement in the physics step, facing in the frame.
    public sealed class AnimalsProcessor : IGameUpdatable, IGameFixedUpdatable
    {
        private const float MinTurnSpeedSqr = 0.01f;
        private const float TurnSharpness = 10f;

        private readonly AnimalRegistry _registry;

        public AnimalsProcessor(AnimalRegistry registry)
        {
            _registry = registry;
        }

        public void OnUpdate(float deltaTime)
        {
            var animals = _registry.Animals;
            for (var i = 0; i < animals.Count; i++)
            {
                FaceVelocity(animals[i], deltaTime);
            }
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            var animals = _registry.Animals;
            for (var i = 0; i < animals.Count; i++)
            {
                animals[i].Movement.OnFixedUpdate(fixedDeltaTime);
            }
        }

        private static void FaceVelocity(AnimalView animal, float deltaTime)
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
