using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;
using ZooWorld.Infrastructure;

namespace ZooWorld.Bootstrap
{
    public sealed class BootstrapFlow : IInitializable, IDisposable
    {
        private readonly CancellationTokenSource _cts = new();
        private readonly ISceneLoader _sceneLoader;

        public BootstrapFlow(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Initialize()
        {
            // We can wait for loading other resources and services later.
            _sceneLoader.LoadAsync(SceneNames.Game, _cts.Token).Forget();
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}
