using UnityEngine;
using ZooWorld.Cameras;
using ZooWorld.Infrastructure;

namespace ZooWorld.Levels
{
    public sealed class ScreenBounds
    {
        private const float TargetViewportPart = 0.5f;

        private readonly Camera _camera;
        private readonly Level _level;
        private readonly IRandomService _random;
        private readonly Plane _ground;

        public ScreenBounds(Level level, CameraCtrl camera, IRandomService random)
        {
            _camera = camera.Camera;
            _level = level;
            _random = random;
            _ground = new Plane(Vector3.up, level.Center);
        }

        public bool Contains(Vector3 position, float viewportInset, float arenaInset)
        {
            var viewport = _camera.WorldToViewportPoint(position);
            var onScreen = viewport.z > 0f
                && viewport.x >= viewportInset && viewport.x <= 1f - viewportInset
                && viewport.y >= viewportInset && viewport.y <= 1f - viewportInset;

            return onScreen && _level.Contains(position, arenaInset);
        }

        public Vector3 Center(float arenaInset)
        {
            return PointAt(0.5f, 0.5f, arenaInset);
        }

        public Vector3 RandomPointInside(float arenaInset)
        {
            var half = TargetViewportPart * 0.5f;
            return PointAt(_random.Range(0.5f - half, 0.5f + half), _random.Range(0.5f - half, 0.5f + half), arenaInset);
        }

        private Vector3 PointAt(float viewportX, float viewportY, float arenaInset)
        {
            var ray = _camera.ViewportPointToRay(new Vector3(viewportX, viewportY, 0f));
            var point = _ground.Raycast(ray, out var distance) ? ray.GetPoint(distance) : _level.Center;
            return _level.Clamp(point, arenaInset);
        }
    }
}
