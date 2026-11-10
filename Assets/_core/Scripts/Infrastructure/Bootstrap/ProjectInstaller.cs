using Zenject;
using ZooWorld.Infrastructure;

namespace ZooWorld.Installers
{
    public sealed class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IGameObjectPool>().To<GameObjectPool>().AsSingle();
            Container.Bind<IRandomService>().To<UnityRandomService>().AsSingle();
        }
    }
}
