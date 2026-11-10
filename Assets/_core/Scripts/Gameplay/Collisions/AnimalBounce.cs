using UnityEngine;
using ZooWorld.Animals;
using ZooWorld.Infrastructure;

namespace ZooWorld.Collisions
{
    public sealed class AnimalBounce
    {
        private const float MinDistanceSqr = 0.0001f;

        private readonly CollisionConfig _config;
        private readonly IRandomService _random;

        public AnimalBounce(CollisionConfig config, IRandomService random)
        {
            _config = config;
            _random = random;
        }

        public void Apply(AnimalView a, AnimalView b)
        {
            var direction = a.Rigidbody.position - b.Rigidbody.position;
            direction.y = 0f;
            direction = direction.sqrMagnitude < MinDistanceSqr ? _random.DirectionXZ() : direction.normalized;

            Push(a, direction);
            Push(b, -direction);
        }

        private void Push(AnimalView animal, Vector3 direction)
        {
            animal.Rigidbody.AddForce(direction * _config.BounceImpulse, ForceMode.Impulse);
            animal.Navigation.Wander(direction);
        }
    }
}
