using UnityEngine;
using Zenject;
using ZooWorld.UI;

namespace ZooWorld.Installers
{
    public sealed class UIInstaller : MonoInstaller
    {
        [SerializeField] private UIGameCtrl _uiGameCtrlPrefab;
        [SerializeField] private WorldPopupsCanvas _worldPopupsCanvasPrefab;
        [SerializeField] private TastyPopupConfig _tastyPopupConfig;

        public override void InstallBindings()
        {
            Container.Bind<UIGameCtrl>().FromComponentInNewPrefab(_uiGameCtrlPrefab).AsSingle().NonLazy();
            Container.Bind<DeathCounterView>()
                .FromMethod(ctx => ctx.Container.Resolve<UIGameCtrl>().Get<DeathCounterView>(GameScreenId.DeathCounter))
                .AsSingle();
            Container.BindInterfacesTo<DeathCounterPresenter>().AsSingle();

            Container.Bind<WorldPopupsCanvas>().FromComponentInNewPrefab(_worldPopupsCanvasPrefab).AsSingle();

            Container.BindInstance(_tastyPopupConfig);
            Container.BindInterfacesTo<TastyPopupPresenter>().AsSingle();
        }
    }
}
