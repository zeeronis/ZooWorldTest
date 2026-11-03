using Zenject;
using ZooWorld.Bootstrap;

namespace ZooWorld.Installers
{
    public sealed class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BootstrapFlow>().AsSingle();
        }
    }
}
