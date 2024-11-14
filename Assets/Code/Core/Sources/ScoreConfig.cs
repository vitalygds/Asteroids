using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [Serializable]
    internal struct ScoreConfig
    {
        [SerializeField] private UnitPointsConfig[] _unitPoints;
        [SerializeField] private int _perSecondValue;
        public IReadOnlyList<UnitPointsConfig> UnitPoints => _unitPoints;
        public int PerSecondValue => _perSecondValue;
    }
}