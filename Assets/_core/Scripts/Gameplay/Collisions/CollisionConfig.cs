using UnityEngine;

namespace ZooWorld.Collisions
{
    [CreateAssetMenu(menuName = "ZooWorld/Collisions/Collision", fileName = "CollisionConfig")]
    public sealed class CollisionConfig : ScriptableObject
    {
        [SerializeField, Min(0f)] private float _bounceImpulse = 5f;

        public float BounceImpulse => _bounceImpulse;
    }
}
