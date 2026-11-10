using UnityEngine;
using ZooWorld.Infrastructure;
using ZooWorld.Movement;

namespace ZooWorld.Animals
{
    public sealed class AnimalFactory
    {
        private readonly IGameObjectPool _pool;
        private readonly AnimalsProcessor _animals;

        public AnimalFactory(IGameObjectPool pool, AnimalsProcessor animals)
        {
            _pool = pool;
            _animals = animals;
        }

        public AnimalCtrl Create(AnimalConfig config, Vector3 position)
        {
            var animal = _pool.Get(config.Prefab).GetComponent<AnimalCtrl>();
            var body = animal.Rigidbody;

            animal.transform.SetPositionAndRotation(position, Quaternion.identity);
            body.position = position;
            body.linearVelocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;

            var navigation = new Navigation(Navigation.RandomDirection());
            animal.Setup(config, navigation, config.Movement.CreateMovement(body, navigation));
            _animals.Register(animal);
            return animal;
        }
    }
}
