using System;
using UnityEngine;

namespace ZooWorld.Animals
{
    [CreateAssetMenu(menuName = "ZooWorld/Animals/Role", fileName = "AnimalRole")]
    public sealed class AnimalRole : ScriptableObject
    {
        [SerializeField] private string _displayName;
        [SerializeField] private AnimalRole[] _eats = Array.Empty<AnimalRole>();

        public string DisplayName => _displayName;

        public bool CanEat(AnimalRole other)
        {
            return Array.IndexOf(_eats, other) >= 0;
        }
    }
}
