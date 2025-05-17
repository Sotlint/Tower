using Microsoft.Xna.Framework;
using Tower.Managers;

namespace Tower.Core.Abstractions.Base;

public interface IHaveCollision
{
    void ResolveCollision();

    Rectangle GetBounds();
}