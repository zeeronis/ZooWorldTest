using UnityEngine;

namespace ZooWorld.Infrastructure
{
    public sealed class UnityRandomService : IRandomService
    {
        public float Value => Random.value;

        public float Range(float min, float max)
        {
            return Random.Range(min, max);
        }

        public int Range(int min, int max)
        {
            return Random.Range(min, max);
        }

        public bool Chance(float probability)
        {
            return Random.value < probability;
        }

        public Vector3 DirectionXZ()
        {
            var angle = Random.Range(0f, 2f * Mathf.PI);
            return new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));
        }
    }
}
