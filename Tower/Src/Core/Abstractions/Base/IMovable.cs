using Microsoft.Xna.Framework;

namespace Tower.Core.Abstractions.Base;

public interface IMovable : IHavePosition
{
    int Speed { get; }
    void Move(Vector2 targetPosition);
}