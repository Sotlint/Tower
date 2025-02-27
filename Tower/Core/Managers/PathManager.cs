using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Tower.Core.Managers;

public class PathManager
{
    public Vector2 CitadelPosition { get; private set; }

    public PathManager(List<Vector2> waypoints, Vector2 citadelPosition)
    {
        CitadelPosition = citadelPosition;
    }

    public Vector2 GetNextWaypoint(int currentWaypointIndex)
    {
        return CitadelPosition;
    }
}