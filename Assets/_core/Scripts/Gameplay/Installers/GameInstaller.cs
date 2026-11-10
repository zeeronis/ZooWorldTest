using UnityEngine;
using Zenject;
using ZooWorld.Animals;
using ZooWorld.Cameras;
using ZooWorld.Infrastructure;
using ZooWorld.Levels;
using ZooWorld.Spawning;

namespace ZooWorld.Installers
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private CameraCtrl _cameraPrefab;
        [SerializeField] private Level _levelPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CameraCtrl>().FromComponentInNewPrefab(_cameraPrefab).AsSingle();
            Container.Bind<LevelBuilder>().AsSingle().WithArguments(_levelPrefab).NonLazy();

            Container.Bind<ScreenBounds>().AsSingle();

            // Update order follows binding order: navigation rules run before movement.
            Container.BindInterfacesTo<ReturnToScreenService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnimalsProcessor>().AsSingle();
            Container.Bind<AnimalFactory>().AsSingle();
            Container.BindInterfacesTo<AnimalSpawner>().AsSingle();

            Container.BindInterfacesTo<GameLoop>().AsSingle().NonLazy();
        }
    }
}
