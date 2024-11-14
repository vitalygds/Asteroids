using System;
using UnityEngine;

namespace UI
{
    public interface IPlayerViewModel : IViewModel
    {
        event Action OnUpdate;
        int Score { get; }
        Vector2 Position { get; }
        Quaternion Rotation { get; }
        float Speed { get; }
        int WeaponCharges { get; }
        int WeaponMaxCharges { get; }
        float ChargeTime { get; }
    }
}