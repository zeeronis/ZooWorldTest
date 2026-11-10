using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ZooWorld.Infrastructure
{
    // The only place that drives the game. Update order = binding order.
    public sealed class GameLoop : ITickable, IFixedTickable
    {
        private readonly List<IGameUpdatable> _updatables;
        private readonly List<IGameFixedUpdatable> _fixedUpdatables;

        public GameLoop(List<IGameUpdatable> updatables, List<IGameFixedUpdatable> fixedUpdatables)
        {
            _updatables = updatables;
            _fixedUpdatables = fixedUpdatables;
        }

        public void Tick()
        {
            var deltaTime = Time.deltaTime;
            for (var i = 0; i < _updatables.Count; i++)
            {
                _updatables[i].OnUpdate(deltaTime);
            }
        }

        public void FixedTick()
        {
            var fixedDeltaTime = Time.fixedDeltaTime;
            for (var i = 0; i < _fixedUpdatables.Count; i++)
            {
                _fixedUpdatables[i].OnFixedUpdate(fixedDeltaTime);
            }
        }
    }
}
