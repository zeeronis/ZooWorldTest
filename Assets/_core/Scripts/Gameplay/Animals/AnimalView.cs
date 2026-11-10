using UnityEngine;
using ZooWorld.Collisions;
using ZooWorld.Movement;

namespace ZooWorld.Animals
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class AnimalView : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        [SerializeField] private Transform _visual;

        private IAnimalCollisionSink _collisions;

        public AnimalConfig Config { get; private set; }
        public Navigation Navigation { get; private set; }
        public IMovement Movement { get; private set; }
        public bool IsAlive { get; private set; }
        public Rigidbody Rigidbody => _rigidbody;
        public Collider Collider => _collider;
        public Transform Visual => _visual;

        public void Setup(AnimalConfig config, Navigation navigation, IMovement movement, IAnimalCollisionSink collisions)
        {
            Config = config;
            Navigation = navigation;
            Movement = movement;
            _collisions = collisions;
            IsAlive = true;
        }

        public void MarkDead()
        {
            IsAlive = false;
        }

        // Only reports the contact. Walls have no Rigidbody, so they never get here.
        private void OnCollisionEnter(Collision collision)
        {
            if (!IsAlive || collision.rigidbody == null)
            {
                return;
            }

            if (collision.rigidbody.TryGetComponent<AnimalView>(out var other) && other.IsAlive)
            {
                _collisions.Report(this, other);
            }
        }
    }
}
