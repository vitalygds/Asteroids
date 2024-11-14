using Infrastructure;
using UnityEngine;

namespace UI
{
    public sealed class UIScope
    {
        public static void Build(IServiceLocator locator, GameObject root)
        {
            MainWindow mainWindow = root.GetComponent<MainWindow>();
            locator.Register<MenuPresenter>(new MenuPresenter(mainWindow.StartWindow, mainWindow.RestartWindow));
            locator.Register<PlayerPresenter>(new PlayerPresenter(mainWindow.PlayerInfoView, locator.Resolve<ITickController>()));
        }
    }
}