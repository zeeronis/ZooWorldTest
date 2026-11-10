using UnityEngine;

namespace ZooWorld.Movement
{
    // Base for movement settings. Each movement kind gets its own config type that creates its strategy.
    public abstract class MovementConfig : ScriptableObject
    {
        public abstract IMovement CreateMovement(Rigidbody body, Navigation navigation);
    }
}
