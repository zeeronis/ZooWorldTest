using UnityEngine;
using ZooWorld.Cameras;

namespace ZooWorld.Levels
{
    // Area the player sees: inside the camera viewport and inside the arena walls.
    public sealed class ScreenBounds
    {
        private const float TargetViewportPart = 0.5f;

        private readonly Camera _camera;
        private readonly Level _level;
        private readonly Plane _ground;

        public ScreenBounds(LevelBuilder levelBuilder, CameraCtrl camera)
        {
            _camera = camera.Camera;
            _level = levelBuilder.Level;
            _ground = new Plane(Vector3.up, _level.transform.position);
        }

        // viewportInset is a share of the screen, arenaInset is in world units.
        public bool Contains(Vector3 position, float viewportInset, float arenaInset)
        {
            var viewport = _camera.WorldToViewportPoint(position);
            var onScreen = viewport.z > 0f
                && viewport.x >= viewportInset && viewport.x <= 1f - viewportInset
                && viewport.y >= viewportInset && viewport.y <= 1f - viewportInset;

            return onScreen && IsInArena(position, arenaInset);
        }

        public Vector3 Center(float arenaInset)
        {
            return PointAt(0.5f, 0.5f, arenaInset);
        }

        // Random point around the screen center, kept inside the arena.
        public Vector3 RandomPointInside(float arenaInset)
        {
            var half = TargetViewportPart * 0.5f;
            return PointAt(Random.Range(0.5f - half, 0.5f + half), Random.Range(0.5f - half, 0.5f + half), arenaInset);
        }

        private Vector3 PointAt(float viewportX, float viewportY, float arenaInset)
        {
            var ray = _camera.ViewportPointToRay(new Vector3(viewportX, viewportY, 0f));
            var point = _ground.Raycast(ray, out var distance) ? ray.GetPoint(distance) : _level.transform.position;
            return ClampToArena(point, arenaInset);
        }

        private bool IsInArena(Vector3 position, float inset)
        {
            var local = position - _level.transform.position;
            var half = _level.ArenaSize * 0.5f - Vector2.one * inset;
            return Mathf.Abs(local.x) <= half.x && Mathf.Abs(local.z) <= half.y;
        }

        private Vector3 ClampToArena(Vector3 position, float inset)
        {
            var center = _level.transform.position;
            var half = Vector2.Max(_level.ArenaSize * 0.5f - Vector2.one * inset, Vector2.zero);
            position.x = Mathf.Clamp(position.x, center.x - half.x, center.x + half.x);
            position.y = center.y;
            position.z = Mathf.Clamp(position.z, center.z - half.y, center.z + half.y);
            return position;
        }
    }
}
