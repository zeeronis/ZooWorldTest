using System;
using ZooWorld.Animals;

namespace ZooWorld.Collisions
{
    public readonly struct AnimalPair : IEquatable<AnimalPair>
    {
        public readonly AnimalView First;
        public readonly AnimalView Second;

        public AnimalPair(AnimalView first, AnimalView second)
        {
            First = first;
            Second = second;
        }

        public bool Equals(AnimalPair other)
        {
            return (First == other.First && Second == other.Second)
                || (First == other.Second && Second == other.First);
        }

        public override bool Equals(object obj)
        {
            return obj is AnimalPair other && Equals(other);
        }

        public override int GetHashCode()
        {
            // Symmetric on purpose.
            return First.GetHashCode() ^ Second.GetHashCode();
        }
    }
}
