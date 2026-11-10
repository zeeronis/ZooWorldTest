using UnityEngine;

namespace ZooWorld.Movement
{
    [CreateAssetMenu(menuName = "ZooWorld/Movement/Linear", fileName = "LinearMovementConfig")]
    public sealed class LinearMovementConfig : MovementConfig
    {
        [SerializeField, Min(0f)] private float _speed = 2f;

        public float Speed => _speed;
    }
}
