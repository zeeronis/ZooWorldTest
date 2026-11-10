using UnityEngine;
using Zenject;
using ZooWorld.Animals;
using ZooWorld.Cameras;
using ZooWorld.Collisions;
using ZooWorld.Feeding;
using ZooWorld.Infrastructure;
using ZooWorld.Levels;
using ZooWorld.Movement;
using ZooWorld.Spawning;
using ZooWorld.Stats;

namespace ZooWorld.Installers
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private CameraCtrl _cameraPrefab;
        [SerializeField] private Level _levelPrefab;
        [SerializeField] private CollisionConfig _collisionConfig;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<AnimalDiedSignal>();
            Container.DeclareSignal<AnimalAteSignal>();

            Container.Bind<Level>().FromComponentInNewPrefab(_levelPrefab).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CameraCtrl>().FromComponentInNewPrefab(_cameraPrefab).AsSingle();
            Container.Bind<ScreenBounds>().AsSingle();

            Container.BindInstance(_collisionConfig);

            Container.Bind<AnimalRegistry>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnimalContacts>().AsSingle();
            Container.Bind<AnimalBounce>().AsSingle();
            Container.Bind<AnimalLifecycle>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeathStats>().AsSingle();

            // Update order follows binding order: feeding first (it may remove animals),
            // then navigation rules, then movement.
            Container.BindInterfacesTo<FeedingSolver>().AsSingle();
            Container.BindInterfacesTo<ReturnToScreenService>().AsSingle();
            Container.BindInterfacesTo<AnimalsProcessor>().AsSingle();
            Container.BindInterfacesTo<AnimalSpawner>().AsSingle();

            Container.BindInterfacesTo<GameLoop>().AsSingle().NonLazy();
        }
    }
}
