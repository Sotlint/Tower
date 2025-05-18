using Microsoft.Xna.Framework;

namespace Tower.Core.Abstractions.Base;

public interface IHaveCollision
{
    void ResolveCollision();
    Rectangle Bounds { get; }
    void UpdateBounds();
}