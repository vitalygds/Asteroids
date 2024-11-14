using System;
using System.Collections.Generic;
using Unit;
using UnityEngine;

namespace Core
{
    [Serializable]
    internal struct UnitSpawnLevelConfig
    {
        [SerializeField, UnitId] private string[] _ids;
        [SerializeField] private int _maxCount;
        [SerializeField] private float _spawnTick;
        [SerializeField] private float _deadZoneBound;

        public IReadOnlyList<string> Ids => _ids;
        public int MaxCount => _maxCount;
        public float SpawnTick => _spawnTick;
        public float DeadZoneBound => _deadZoneBound;
    }
}