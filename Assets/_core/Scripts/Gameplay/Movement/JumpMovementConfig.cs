using UnityEngine;

namespace ZooWorld.Movement
{
    [CreateAssetMenu(menuName = "ZooWorld/Movement/Jump", fileName = "JumpMovementConfig")]
    public sealed class JumpMovementConfig : MovementConfig
    {
        [SerializeField, Min(0.1f)] private float _jumpInterval = 1f;
        [SerializeField, Min(0f)] private float _jumpDistance = 2f;

        public float JumpInterval => _jumpInterval;
        public float JumpDistance => _jumpDistance;
    }
}
