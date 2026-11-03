using System.Threading;
using Cysharp.Threading.Tasks;

namespace ZooWorld.Infrastructure
{
    public interface ISceneLoader
    {
        UniTask LoadAsync(string sceneName, CancellationToken token);
    }
}
