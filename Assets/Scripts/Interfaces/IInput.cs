using System;
using System.Numerics;

namespace DefaultNamespace
{
    public interface IInput
    {
        event Action<Vector2> MoveDirection;
        event Action Jump;
    }
}