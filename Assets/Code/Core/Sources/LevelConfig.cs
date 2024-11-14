using Unit;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(LevelConfig), fileName = nameof(LevelConfig))]
    internal sealed class LevelConfig : ScriptableObject
    {
        [SerializeField, UnitId] private string _playerId;
        [SerializeField] private UnitSpawnLevelConfig _asteroidConfig;
        [SerializeField] private UnitSpawnLevelConfig _ufoConfig;
        [SerializeField] private ScoreConfig _scoreConfig;
        [SerializeField] private bool _asteroidsExistAlways;
        public string PlayerId => _playerId;
        public UnitSpawnLevelConfig AsteroidConfig => _asteroidConfig;
        public UnitSpawnLevelConfig UfoConfig => _ufoConfig;
        public ScoreConfig ScoreConfig => _scoreConfig;
        public bool AsteroidsExistAlways => _asteroidsExistAlways;
    }
}