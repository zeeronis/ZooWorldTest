using UnityEngine;
using Zenject;
using ZooWorld.UI;

namespace ZooWorld.Installers
{
    public sealed class UIInstaller : MonoInstaller
    {
        [SerializeField] private UIGameCtrl _uiGameCtrlPrefab;

        public override void InstallBindings()
        {
            Container.Bind<UIGameCtrl>().FromComponentInNewPrefab(_uiGameCtrlPrefab).AsSingle().NonLazy();
        }
    }
}
