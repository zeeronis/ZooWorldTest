using System;
using System.Collections.Generic;
using Zenject;
using ZooWorld.Animals;

namespace ZooWorld.Stats
{
    public sealed class DeathStats : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly Dictionary<AnimalConfig, int> _counts = new();
        private readonly List<AnimalConfig> _kinds = new();

        public event Action<AnimalConfig, int> Changed;

        public DeathStats(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public IReadOnlyList<AnimalConfig> Kinds => _kinds;

        public int GetCount(AnimalConfig kind)
        {
            return _counts.TryGetValue(kind, out var count) ? count : 0;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<AnimalDiedSignal>(OnDied);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<AnimalDiedSignal>(OnDied);
        }

        private void OnDied(AnimalDiedSignal signal)
        {
            var kind = signal.Config;
            if (!_counts.TryGetValue(kind, out var count))
            {
                _kinds.Add(kind);
            }

            _counts[kind] = ++count;
            Changed?.Invoke(kind, count);
        }
    }
}
