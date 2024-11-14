using Infrastructure;
using Input;
using UI;
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
        [SerializeField] private GameObject _interface;
        [SerializeField] private FieldViewProvider _fieldViewProvider;
        [SerializeField] private LevelConfig _startLevel;
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
            _locator.Register<IFieldViewProvider>(_fieldViewProvider);
            InfrastructureScope.Build(_locator);
            InputScope.Build(_locator);
            UnitScope.Build(_locator);
            WeaponScope.Build(_locator);
            UIScope.Build(_locator, _interface);
            UnitScope.Complete(_locator);

            _fieldViewProvider.Initialize(Camera.main);
            StartGame();
        }

        private void StartGame()
        {
            IUnitSpawner unitSpawner = _locator.Resolve<IUnitSpawner>();
            IUnitManager unitManager = _locator.Resolve<IUnitManager>();
            IFieldViewProvider fieldViewProvider = _locator.Resolve<IFieldViewProvider>();
            IRandomizer randomizer = _locator.Resolve<IRandomizer>();
            ITickController tickController = _locator.Resolve<ITickController>();
            IUnitDamageService damageService = _locator.Resolve<IUnitDamageService>();
            IChargeableModelMap chargeableModelMap = _locator.Resolve<IChargeableModelMap>();

            PlayerController playerController = new PlayerController(new PlayerInputController(_locator.Resolve<IInputService>()), unitSpawner,
                fieldViewProvider, tickController);
            UnitSpawnController asteroidController = new UnitSpawnController(unitSpawner, unitManager, tickController, randomizer, fieldViewProvider);
            UfoSpawnController ufoController = new UfoSpawnController(unitSpawner, unitManager, tickController, randomizer, fieldViewProvider);

            PlayerScoreController scoreController = new PlayerScoreController(tickController, damageService, chargeableModelMap);
            MenuPresenter menuPresenter = _locator.Resolve<MenuPresenter>();
            PlayerPresenter playerPresenter = _locator.Resolve<PlayerPresenter>();
            GameController gameController = new GameController(menuPresenter, playerPresenter, playerController, asteroidController, ufoController,
                scoreController);
            _eventSystem.gameObject.SetActive(true);

            gameController.Start(_startLevel);
        }

        private void OnDestroy()
        {
            _locator?.Dispose();
        }
    }
}