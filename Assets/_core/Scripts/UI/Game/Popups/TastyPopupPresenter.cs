using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ZooWorld.Animals;
using ZooWorld.Cameras;
using ZooWorld.Infrastructure;

namespace ZooWorld.UI
{
    public sealed class TastyPopupPresenter : IInitializable, IDisposable, IGameUpdatable
    {
        private struct Popup
        {
            public TastyPopupView View;
            public Vector3 Origin;
            public float Age;
        }

        private readonly List<Popup> _popups = new();
        private readonly SignalBus _signalBus;
        private readonly IGameObjectPool _pool;
        private readonly Camera _camera;
        private readonly WorldPopupsCanvas _canvas;
        private readonly TastyPopupConfig _config;

        public TastyPopupPresenter(SignalBus signalBus, IGameObjectPool pool, CameraCtrl camera, WorldPopupsCanvas canvas, TastyPopupConfig config)
        {
            _signalBus = signalBus;
            _pool = pool;
            _camera = camera.Camera;
            _canvas = canvas;
            _config = config;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<AnimalAteSignal>(OnAte);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<AnimalAteSignal>(OnAte);
            // Views stay under the canvas and are destroyed with the scene; the pool skips destroyed ones.
            _popups.Clear();
        }

        public void OnUpdate(float deltaTime)
        {
            for (var i = _popups.Count - 1; i >= 0; i--)
            {
                var popup = _popups[i];
                popup.Age += deltaTime;
                if (popup.Age >= _config.Duration)
                {
                    Release(i);
                    continue;
                }

                _popups[i] = popup;
                Place(popup);
            }
        }

        private void OnAte(AnimalAteSignal signal)
        {
            var view = _pool.Get(_config.Prefab.gameObject).GetComponent<TastyPopupView>();
            view.Setup(_canvas.Root, _config.Text);

            var popup = new Popup
            {
                View = view,
                Origin = signal.Predator.transform.position + _config.SpawnOffset,
                Age = 0f,
            };

            _popups.Add(popup);
            Place(popup);
        }

        private void Place(Popup popup)
        {
            var progress = popup.Age / _config.Duration;
            var position = popup.Origin + Vector3.up * (_config.RiseHeight * progress);

            // Billboard: same orientation as the camera, so the text faces it and stays upright on screen.
            popup.View.SetPose(position, _camera.transform.rotation);
            popup.View.SetAlpha(Fade(progress));
        }

        private float Fade(float progress)
        {
            var fadeStart = 1f - _config.FadePart;
            return progress <= fadeStart ? 1f : Mathf.Clamp01((1f - progress) / Mathf.Max(_config.FadePart, 0.0001f));
        }

        private void Release(int index)
        {
            var view = _popups[index].View;
            _popups.RemoveAt(index);
            if (view != null)
            {
                _pool.Release(view.gameObject);
            }
        }
    }
}
