using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ZooWorld.UI
{
    // Simplest implementation. No binding between peresenters and views.
    public abstract class UIController<TScreenId> : MonoBehaviour where TScreenId : struct, Enum
    {
        [Serializable]
        private struct Screen
        {
            public TScreenId Id;
            public View Prefab;
        }

        [SerializeField] private Transform _root;
        [SerializeField] private Screen[] _screens;

        private readonly Dictionary<TScreenId, View> _views = new();
        private DiContainer _container;

        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }

        public void Open(TScreenId id)
        {
            Get<View>(id).Open();
        }

        public void Close(TScreenId id)
        {
            Get<View>(id).Close();
        }

        public TView Get<TView>(TScreenId id) where TView : View
        {
            if (!_views.TryGetValue(id, out var view))
            {
                var prefab = Array.Find(_screens, screen => EqualityComparer<TScreenId>.Default.Equals(screen.Id, id)).Prefab;
                view = _container.InstantiatePrefabForComponent<View>(prefab, _root);
                _views[id] = view;
                view.Create();
            }

            return (TView)view;
        }
    }
}
