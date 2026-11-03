using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace ZooWorld.Infrastructure
{
    public sealed class SceneLoader : ISceneLoader
    {
        public async UniTask LoadAsync(string sceneName, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single).ToUniTask(cancellationToken: token);
        }
    }
}
