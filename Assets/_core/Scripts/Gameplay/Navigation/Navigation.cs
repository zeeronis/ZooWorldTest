using UnityEngine;

namespace ZooWorld.Movement
{
    public sealed class Navigation
    {
        private Vector3 _wander;
        private Vector3? _forced;

        public Navigation(Vector3 direction)
        {
            _wander = direction;
        }

        public Vector3 Direction => _forced ?? _wander;
        public bool IsForced => _forced.HasValue;

        public void Wander(Vector3 direction)
        {
            _wander = direction;
        }

        public void Force(Vector3 direction)
        {
            _forced = direction;
        }

        public void Release()
        {
            if (_forced.HasValue)
            {
                _wander = _forced.Value;
                _forced = null;
            }
        }
    }
}
