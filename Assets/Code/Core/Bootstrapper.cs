using Infrastructure;
using Input;
using Unit;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using Weapon;

namespace Core
{
    internal sealed class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private InputSystemUIInputModule _inputModule;
        private ServiceLocator _locator;

        private void Start()
        {
            //знаю, что сервис-локатор - антипаттерн, но раз нельзя использовать сторонние фреймворки,
            //а тратить время на кастомный DI - оверхед, то вот=))
            //Так бы юзал VContainer, к слову
            DontDestroyOnLoad(this);
            _locator = new ServiceLocator();
            _locator.Register<EventSystem>(_eventSystem);
            _locator.Register<InputSystemUIInputModule>(_inputModule);
            InfrastructureScope.Build(_locator);
            InputScope.Build(_locator);
            UnitScope.Build(_locator);
            WeaponScope.Build(_locator);
            UnitScope.Complete(_locator);


            _eventSystem.gameObject.SetActive(true);
        }
        
        private void OnDestroy()
        {
            _locator?.Dispose();
        }
    }
}