using UnityEngine;
using Zenject;
using ZooWorld.Collisions;
using ZooWorld.Infrastructure;
using ZooWorld.Movement;

namespace ZooWorld.Animals
{
    public sealed class AnimalLifecycle
    {
        private readonly IGameObjectPool _pool;
        private readonly AnimalRegistry _registry;
        private readonly IAnimalCollisionSink _collisions;
        private readonly IRandomService _random;
        private readonly SignalBus _signalBus;

        public AnimalLifecycle(IGameObjectPool pool, AnimalRegistry registry, IAnimalCollisionSink collisions, IRandomService random, SignalBus signalBus)
        {
            _pool = pool;
            _registry = registry;
            _collisions = collisions;
            _random = random;
            _signalBus = signalBus;
        }

        public AnimalView Spawn(AnimalConfig config, Vector3 position)
        {
            var animal = _pool.Get(config.Prefab).GetComponent<AnimalView>();
            var body = animal.Rigidbody;

            animal.transform.SetPositionAndRotation(position, Quaternion.identity);
            body.position = position;
            body.linearVelocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;

            var navigation = new Navigation(_random.DirectionXZ());
            animal.Setup(config, navigation, config.Movement.CreateMovement(body, navigation, _random), _collisions);
            _registry.Add(animal);
            return animal;
        }

        public void Despawn(AnimalView animal)
        {
            if (!_registry.Remove(animal))
            {
                return;
            }

            // Fired before the animal goes back to the pool, see AnimalDiedSignal.
            _signalBus.Fire(new AnimalDiedSignal(animal));
            animal.MarkDead();
            _pool.Release(animal.gameObject);
        }
    }
}
