using UnityEngine;
using UnityEngine.InputSystem;
using ZooWorld.Infrastructure;

namespace ZooWorld.Cameras
{
    public sealed class CameraCtrl : MonoBehaviour, IGameUpdatable
    {
        [SerializeField] private Camera _camera;
        [SerializeField, Min(0f)] private float _padding = 2f;

        private Vector2 _areaBounds;
        private Vector3 _origin;
        private Plane _ground;
        private Vector3 _grabPoint;
        private bool _isDragging;

        public Camera Camera => _camera;

        public void Setup(Vector3 areaCenter, Vector2 areaBounds)
        {
            _origin = areaCenter;
            _areaBounds = areaBounds;
            _ground = new Plane(Vector3.up, areaCenter);
            transform.position = areaCenter;
        }

        public void OnUpdate(float deltaTime)
        {
            var pointer = Pointer.current;
            if (pointer == null || !pointer.press.isPressed)
            {
                _isDragging = false;
                return;
            }

            if (!TryGetGroundPoint(pointer.position.ReadValue(), out var point))
            {
                return;
            }

            if (!_isDragging)
            {
                _isDragging = true;
                _grabPoint = point;
                return;
            }

            MoveTo(transform.position + _grabPoint - point);
        }

        public void MoveTo(Vector3 position)
        {
            var limit = _areaBounds * 0.5f + Vector2.one * _padding;
            position.x = Mathf.Clamp(position.x, _origin.x - limit.x, _origin.x + limit.x);
            position.y = _origin.y;
            position.z = Mathf.Clamp(position.z, _origin.z - limit.y, _origin.z + limit.y);
            transform.position = position;
        }

        private bool TryGetGroundPoint(Vector2 screenPoint, out Vector3 point)
        {
            var ray = _camera.ScreenPointToRay(screenPoint);
            if (_ground.Raycast(ray, out var distance))
            {
                point = ray.GetPoint(distance);
                return true;
            }

            point = default;
            return false;
        }
    }
}
