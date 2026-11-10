using UnityEngine;
using ZooWorld.Infrastructure;
using ZooWorld.Spawning;

namespace ZooWorld.Levels
{
    public sealed class Level : MonoBehaviour
    {
        [SerializeField] private SpawnConfig _spawnConfig;

        [Tooltip("Playable area on XZ, centered on the level root (inner faces of the walls).")]
        [SerializeField] private Vector2 _arenaSize = new(18f, 9.4f);

        public SpawnConfig SpawnConfig => _spawnConfig;
        public Vector2 ArenaSize => _arenaSize;
        public Vector3 Center => transform.position;

        public bool Contains(Vector3 position, float inset)
        {
            var local = position - Center;
            var half = HalfSize(inset);
            return Mathf.Abs(local.x) <= half.x && Mathf.Abs(local.z) <= half.y;
        }

        public Vector3 Clamp(Vector3 position, float inset)
        {
            var center = Center;
            var half = Vector2.Max(HalfSize(inset), Vector2.zero);
            position.x = Mathf.Clamp(position.x, center.x - half.x, center.x + half.x);
            position.y = center.y;
            position.z = Mathf.Clamp(position.z, center.z - half.y, center.z + half.y);
            return position;
        }

        public Vector3 RandomPoint(float inset, IRandomService random)
        {
            var half = Vector2.Max(HalfSize(inset), Vector2.zero);
            return Center + new Vector3(random.Range(-half.x, half.x), 0f, random.Range(-half.y, half.y));
        }

        private Vector2 HalfSize(float inset)
        {
            return _arenaSize * 0.5f - Vector2.one * inset;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position, new Vector3(_arenaSize.x, 0f, _arenaSize.y));
        }
    }
}
