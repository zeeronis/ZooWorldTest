using UnityEngine;

namespace ZooWorld.Infrastructure
{
    public interface IGameObjectPool
    {
        GameObject Get(GameObject prefab);
        void Release(GameObject instance);
    }
}
