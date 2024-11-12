﻿using UnityEngine;

namespace Input
{
    public interface IPlayerInputListener : IInputListener
    {
        void Move(Vector2 value);
        void OnFire(int value, bool isPressed);
    }
}