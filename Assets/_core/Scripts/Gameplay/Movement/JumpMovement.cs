using UnityEngine;

namespace ZooWorld.Movement
{
    // PhysX damps before integrating: v *= 1 - k * dt, so the body travels v0 * (1 - k * dt) / k.
    public sealed class JumpMovement : IMovement
    {
        private readonly JumpMovementConfig _config;
        private readonly Rigidbody _body;
        private readonly Navigation _navigation;
        private float _timer;

        public JumpMovement(JumpMovementConfig config, Rigidbody body, Navigation navigation)
        {
            _config = config;
            _body = body;
            _navigation = navigation;
            _body.linearDamping = config.Damping;
            _timer = Random.Range(0f, config.JumpInterval);
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            _timer -= fixedDeltaTime;
            if (_timer > 0f)
            {
                return;
            }

            _timer += _config.JumpInterval;
            _navigation.Wander(Navigation.RandomDirection());

            var damping = _config.Damping;
            var speed = _config.JumpDistance * damping / Mathf.Max(0.01f, 1f - damping * fixedDeltaTime);
            _body.linearVelocity = _navigation.Direction * speed;
        }
    }
}
