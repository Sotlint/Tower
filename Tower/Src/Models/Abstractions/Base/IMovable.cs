using Microsoft.Xna.Framework;

namespace Tower.Models.Abstractions.Base;

public interface IMovable : IHavePosition
{
    int Speed { get; }
    void Move(Vector2 targetPosition);
}