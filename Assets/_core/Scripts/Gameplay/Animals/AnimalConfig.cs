using UnityEngine;
using ZooWorld.Movement;

namespace ZooWorld.Animals
{
    [CreateAssetMenu(menuName = "ZooWorld/Animals/Animal", fileName = "AnimalConfig")]
    public sealed class AnimalConfig : ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private AnimalRole _role;
        [SerializeField] private MovementConfig _movement;

        public GameObject Prefab => _prefab;
        public AnimalRole Role => _role;
        public MovementConfig Movement => _movement;
    }
}
