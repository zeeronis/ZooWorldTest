using UnityEngine;

namespace ZooWorld.Infrastructure
{
    // Source of randomness for game rules, so they can be tested with a fixed sequence.
    public interface IRandomService
    {
        // Random float in [0, 1].
        float Value { get; }

        float Range(float min, float max);

        // Max is exclusive.
        int Range(int min, int max);

        bool Chance(float probability);

        // Random unit vector on the XZ plane.
        Vector3 DirectionXZ();
    }
}
