using UnityEngine;

namespace ZooWorld.Animals
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class AnimalCtrl : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _visual;

        public AnimalConfig Config { get; private set; }
        public Rigidbody Rigidbody => _rigidbody;
        public Transform Visual => _visual;

        public void Setup(AnimalConfig config)
        {
            Config = config;
        }

        private void Reset()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }
}
