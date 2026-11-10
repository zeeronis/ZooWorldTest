using System.Collections.Generic;

namespace ZooWorld.Animals
{
    public sealed class AnimalRegistry
    {
        private readonly List<AnimalView> _animals = new();
        private readonly Dictionary<AnimalConfig, int> _countByConfig = new();

        public IReadOnlyList<AnimalView> Animals => _animals;

        public int CountOf(AnimalConfig config)
        {
            return _countByConfig.TryGetValue(config, out var count) ? count : 0;
        }

        public void Add(AnimalView animal)
        {
            _animals.Add(animal);
            _countByConfig[animal.Config] = CountOf(animal.Config) + 1;
        }

        public bool Remove(AnimalView animal)
        {
            if (!_animals.Remove(animal))
            {
                return false;
            }

            _countByConfig[animal.Config]--;
            return true;
        }
    }
}
