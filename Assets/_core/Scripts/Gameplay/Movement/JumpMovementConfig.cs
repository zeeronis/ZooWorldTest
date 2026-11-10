using UnityEngine;
using ZooWorld.Infrastructure;

namespace ZooWorld.Movement
{
    [CreateAssetMenu(menuName = "ZooWorld/Movement/Jump", fileName = "JumpMovementConfig")]
    public sealed class JumpMovementConfig : MovementConfig
    {
        [SerializeField, Min(0.1f)] private float _jumpInterval = 1f;
        [SerializeField, Min(0f)] private float _jumpDistance = 2f;

        [Tooltip("Linear damping during the jump. Higher = shorter and sharper jump.")]
        [SerializeField, Min(0.1f)] private float _damping = 5f;

        public float JumpInterval => _jumpInterval;
        public float JumpDistance => _jumpDistance;
        public float Damping => _damping;

        public override IMovement CreateMovement(Rigidbody body, Navigation navigation, IRandomService random)
        {
            return new JumpMovement(this, body, navigation, random);
        }
    }
}
