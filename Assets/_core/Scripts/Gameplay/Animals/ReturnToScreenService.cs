using UnityEngine;
using ZooWorld.Infrastructure;
using ZooWorld.Levels;
using ZooWorld.Movement;

namespace ZooWorld.Animals
{
    // Animals that leave the visible area are sent back. Knows nothing about how they move.
    public sealed class ReturnToScreenService : IGameFixedUpdatable
    {
        // Different thresholds for leaving and coming back, so the state does not flicker at the edge.
        private const float LeaveViewportInset = 0.02f;
        private const float LeaveArenaInset = 0.7f;
        private const float BackViewportInset = 0.1f;
        private const float BackArenaInset = 1.5f;

        private readonly AnimalsProcessor _animals;
        private readonly ScreenBounds _screen;

        public ReturnToScreenService(AnimalsProcessor animals, ScreenBounds screen)
        {
            _animals = animals;
            _screen = screen;
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            var animals = _animals.Animals;
            for (var i = 0; i < animals.Count; i++)
            {
                var animal = animals[i];
                var navigation = animal.Navigation;
                var position = animal.Rigidbody.position;

                if (!navigation.IsForced)
                {
                    if (!_screen.Contains(position, LeaveViewportInset, LeaveArenaInset))
                    {
                        SendBack(navigation, position);
                    }
                }
                else if (_screen.Contains(position, BackViewportInset, BackArenaInset))
                {
                    navigation.Release();
                }
                else if (Vector3.Dot(navigation.Direction, _screen.Center(BackArenaInset) - position) < 0f)
                {
                    // The camera moved while the animal was coming back.
                    SendBack(navigation, position);
                }
            }
        }

        private void SendBack(Navigation navigation, Vector3 position)
        {
            var direction = _screen.RandomPointInside(BackArenaInset) - position;
            direction.y = 0f;
            if (direction.sqrMagnitude > 0.0001f)
            {
                navigation.Force(direction.normalized);
            }
        }
    }
}
