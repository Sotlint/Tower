using Microsoft.Xna.Framework;

namespace Tower.Models.Abstractions.Base;

public interface IHavePosition
{
    Vector2 Position { get; }
}