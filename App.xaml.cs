using System.Windows;
using Prism.Ioc;
using Prism.Unity;
using StarWarsApiApp.Views;

namespace StarWarsApiApp {

    public partial class App : PrismApplication  {
        protected override Window CreateShell() {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry) {
        }
    }
}
