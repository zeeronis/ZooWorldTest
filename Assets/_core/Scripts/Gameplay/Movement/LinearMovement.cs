using UnityEngine;

namespace ZooWorld.Movement
{
    public sealed class LinearMovement : IMovement
    {
        private readonly LinearMovementConfig _config;
        private readonly Rigidbody _body;
        private readonly Navigation _navigation;

        public LinearMovement(LinearMovementConfig config, Rigidbody body, Navigation navigation)
        {
            _config = config;
            _body = body;
            _navigation = navigation;
            _body.linearDamping = 0f;
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            _body.linearVelocity = _navigation.Direction * _config.Speed;
        }
    }
}
