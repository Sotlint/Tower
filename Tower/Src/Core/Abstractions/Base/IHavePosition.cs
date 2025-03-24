using Microsoft.Xna.Framework;

namespace Tower.Core.Abstractions.Base;

public interface IHavePosition
{
    Vector2 Position { get; }

    void SetPosition(Vector2 direction);
}