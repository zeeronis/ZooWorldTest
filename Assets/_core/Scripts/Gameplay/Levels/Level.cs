using UnityEngine;
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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position, new Vector3(_arenaSize.x, 0f, _arenaSize.y));
        }
    }
}
