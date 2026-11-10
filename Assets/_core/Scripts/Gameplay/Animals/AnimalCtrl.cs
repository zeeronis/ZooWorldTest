using UnityEngine;
using ZooWorld.Movement;

namespace ZooWorld.Animals
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class AnimalCtrl : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        [SerializeField] private Transform _visual;

        public AnimalConfig Config { get; private set; }
        public Navigation Navigation { get; private set; }
        public IMovement Movement { get; private set; }
        public Rigidbody Rigidbody => _rigidbody;
        public Collider Collider => _collider;
        public Transform Visual => _visual;

        public void Setup(AnimalConfig config, Navigation navigation, IMovement movement)
        {
            Config = config;
            Navigation = navigation;
            Movement = movement;
        }
    }
}
