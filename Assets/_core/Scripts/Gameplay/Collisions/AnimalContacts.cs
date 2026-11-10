using System.Collections.Generic;
using ZooWorld.Animals;

namespace ZooWorld.Collisions
{
    public sealed class AnimalContacts : IAnimalCollisionSink
    {
        private readonly List<AnimalPair> _pairs = new();
        private readonly HashSet<AnimalPair> _known = new();

        public IReadOnlyList<AnimalPair> Pairs => _pairs;

        public void Report(AnimalView self, AnimalView other)
        {
            if (self == other)
            {
                return;
            }

            var pair = new AnimalPair(self, other);
            if (_known.Add(pair))
            {
                _pairs.Add(pair);
            }
        }

        public void Clear()
        {
            _pairs.Clear();
            _known.Clear();
        }
    }
}
