using System;
using Unit;
using UnityEngine;

namespace Core
{
    [Serializable]
    internal struct UnitPointsConfig
    {
        [SerializeField, UnitId] private string _unitId;
        [SerializeField] private int _value;

        public string UnitId => _unitId;
        public int Value => _value;
    }
}