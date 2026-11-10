using UnityEngine;

namespace ZooWorld.Movement
{
    // Where the animal wants to go. Movements only read Direction; outside rules may force it.
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

        // Keeps the forced heading, so the animal does not turn straight back.
        public void Release()
        {
            if (_forced.HasValue)
            {
                _wander = _forced.Value;
                _forced = null;
            }
        }

        public static Vector3 RandomDirection()
        {
            var angle = Random.Range(0f, 2f * Mathf.PI);
            return new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));
        }
    }
}
