using UnityEngine;
using ZooWorld.Movement;

namespace ZooWorld.Animals
{
    [CreateAssetMenu(menuName = "ZooWorld/Animals/Animal", fileName = "AnimalConfig")]
    public sealed class AnimalConfig : ScriptableObject
    {
        [Tooltip("Name shown in UI. Falls back to the asset name when empty.")]
        [SerializeField] private string _displayName;
        [Tooltip("Optional icon for UI.")]
        [SerializeField] private Sprite _icon;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private AnimalRole _role;
        [SerializeField] private MovementConfig _movement;

        public string DisplayName => string.IsNullOrEmpty(_displayName) ? name : _displayName;
        public Sprite Icon => _icon;
        public GameObject Prefab => _prefab;
        public AnimalRole Role => _role;
        public MovementConfig Movement => _movement;
    }
}
