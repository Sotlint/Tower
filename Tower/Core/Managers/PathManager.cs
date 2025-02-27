using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Tower.Core.Managers;

public class PathManager
{
    public Vector2? CitadelPosition { get; private set; }

    public Vector2? GetCitadelPosition()
        => CitadelPosition;
    
    public void SetCitadelPosition(Vector2 position)
        => CitadelPosition = position;
}