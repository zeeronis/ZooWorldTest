using UnityEngine;
using ZooWorld.Infrastructure;

namespace ZooWorld.Movement
{
    [CreateAssetMenu(menuName = "ZooWorld/Movement/Linear", fileName = "LinearMovementConfig")]
    public sealed class LinearMovementConfig : MovementConfig
    {
        [SerializeField, Min(0f)] private float _speed = 2f;

        public float Speed => _speed;

        public override IMovement CreateMovement(Rigidbody body, Navigation navigation, IRandomService random)
        {
            return new LinearMovement(this, body, navigation);
        }
    }
}
