using System;
using Input;
using UI;
using Unit;
using UnityEngine;

namespace Core
{
    internal sealed class GameController : IDisposable
    {
        private readonly MenuPresenter _menuPresenter;
        private readonly PlayerPresenter _playerPresenter;
        private readonly PlayerController _playerController;
        private readonly UnitSpawnController _asteroidController;
        private readonly UfoSpawnController _ufoController;
        private readonly PlayerScoreController _scoreController;
        private readonly PlayerInputController _inputController;
        private LevelConfig _currentLevel;

        public GameController(MenuPresenter menuPresenter, PlayerPresenter playerPresenter, PlayerController playerController,
            UnitSpawnController asteroidController, UfoSpawnController ufoController, PlayerScoreController scoreController)
        {
            _menuPresenter = menuPresenter;
            _playerPresenter = playerPresenter;
            _playerController = playerController;
            _asteroidController = asteroidController;
            _ufoController = ufoController;
            _scoreController = scoreController;
        }
        
        public void Start(LevelConfig levelConfig)
        {
            _currentLevel = levelConfig;
            _asteroidController.Start(levelConfig.AsteroidConfig);
            _menuPresenter.Show(StartGame);
        }

        public void Dispose()
        {
            _inputController.Dispose();
            _playerController.Dispose();
            _asteroidController.Dispose();
            _ufoController.Dispose();
        }

        private void StartGame()
        {
            _menuPresenter.Hide();
            IUnit player = _playerController.Start(_currentLevel.PlayerId);
            player.OnDestroy += OnPlayerDied;
            _scoreController.Start(player, _currentLevel.ScoreConfig);
            _playerPresenter.Show(_scoreController);
            _ufoController.SetTarget(player.Transform);
            _ufoController.Start(_currentLevel.UfoConfig);
        }

        private void OnPlayerDied(IUnit unit)
        {
            unit.OnDestroy -= OnPlayerDied;
            _playerPresenter.Hide();
            _scoreController.Stop();
            _ufoController.Stop();
            _menuPresenter.Show(StartGame, _scoreController.Score);
        }
    }
}