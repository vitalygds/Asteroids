using System;
using UnityEngine;

namespace Core
{
    public interface IFieldViewProvider
    {
        event Action<IFieldViewProvider> OnUpdate;
        Vector2 BottomLeft { get; }
        Vector2 TopRight { get; }
    }
}