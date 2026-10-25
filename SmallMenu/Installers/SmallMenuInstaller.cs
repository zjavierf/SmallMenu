using SmallMenu.Configuration;
using SmallMenu.UI;
using Zenject;

namespace SmallMenu.Installers
{
    internal class SmallMenuInstaller : Installer
    {
        private readonly Settings _settings;

        public SmallMenuInstaller(Settings settings)
        {
            _settings = settings;
        }

        public override void InstallBindings()
        {
            Container.Bind<Settings>().FromInstance(_settings);
            Container.BindInterfacesAndSelfTo<MenuScaleController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SettingsHost>().AsSingle().NonLazy();
        }
    }
}