using UnityEngine;
using VContainer;

namespace UI
{
    public sealed class UIScope
    {
        public static void Build(IContainerBuilder builder, GameObject root)
        {
            MainWindow mainWindow = root.GetComponent<MainWindow>();
            builder.RegisterInstance<StartWindow>(mainWindow.StartWindow);
            builder.RegisterInstance<RestartWindow>(mainWindow.RestartWindow);
            builder.RegisterInstance<PlayerInfoView>(mainWindow.PlayerInfoView);
            builder.Register<MenuPresenter>(Lifetime.Singleton);
            builder.Register<PlayerPresenter>(Lifetime.Singleton);
        }
    }
}