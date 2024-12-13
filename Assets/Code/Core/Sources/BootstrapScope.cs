using Infrastructure;
using Input;
using UI;
using Unit;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using VContainer;
using VContainer.Unity;
using Weapon;

namespace Core
{
    internal sealed class BootstrapScope : LifetimeScope
    {
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private InputSystemUIInputModule _inputModule;
        [SerializeField] private GameObject _interface;
        [SerializeField] private FieldViewProvider _fieldViewProvider;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance<EventSystem>(_eventSystem);
            builder.RegisterInstance<InputSystemUIInputModule>(_inputModule);
            builder.RegisterInstance<IFieldViewProvider, FieldViewProvider>(_fieldViewProvider);

            builder.Register<PlayerInputController>(Lifetime.Singleton);
            builder.Register<PlayerController>(Lifetime.Singleton);
            builder.Register<UnitSpawnController>(Lifetime.Singleton);
            builder.Register<UfoSpawnController>(Lifetime.Singleton);
            builder.Register<PlayerScoreController>(Lifetime.Singleton);
            builder.Register<GameController>(Lifetime.Singleton);
            
            InfrastructureScope.Build(builder);
            InputScope.Build(builder);
            UnitScope.Build(builder);
            WeaponScope.Build(builder);
            UIScope.Build(builder, _interface);
        }
    }
}