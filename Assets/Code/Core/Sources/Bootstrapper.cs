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
    internal sealed class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private LevelConfig _startLevel;
        private LifetimeScope _scope;

        private void Start()
        {
            DontDestroyOnLoad(this);
            _scope = GetComponent<BootstrapScope>();
            _scope.Build();
        }

        [Inject]
        private void Build(IObjectResolver resolver, EventSystem eventSystem, FieldViewProvider fieldViewProvider,
            UnitServiceResolver serviceResolver, GameController gameController)
        {
            fieldViewProvider.Initialize(Camera.main);
            eventSystem.gameObject.SetActive(true);
            gameController.Start(_startLevel);
        }
    }
}