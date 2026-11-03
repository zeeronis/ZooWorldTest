using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ZooWorld.Infrastructure
{
    // Simplest pool implementation
    public sealed class GameObjectPool : IGameObjectPool
    {
        private readonly Dictionary<GameObject, Stack<GameObject>> _freeByPrefab = new();
        private readonly Dictionary<GameObject, GameObject> _prefabByInstance = new();
        private readonly DiContainer _container;

        public GameObjectPool(DiContainer container)
        {
            _container = container;
        }

        public GameObject Get(GameObject prefab)
        {
            if (!_freeByPrefab.TryGetValue(prefab, out var free))
            {
                free = new Stack<GameObject>();
                _freeByPrefab[prefab] = free;
            }

            while (free.TryPop(out var instance))
            {
                // Null if a caller parented it under a scene object that has been unloaded.
                if (instance != null)
                {
                    instance.SetActive(true);
                    return instance;
                }
            }

            var created = _container.InstantiatePrefab(prefab);
            _prefabByInstance[created] = prefab;
            return created;
        }

        public void Release(GameObject instance)
        {
            instance.SetActive(false);
            _freeByPrefab[_prefabByInstance[instance]].Push(instance);
        }
    }
}
