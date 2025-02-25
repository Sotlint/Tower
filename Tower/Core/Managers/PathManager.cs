using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Tower.Core.Managers;

public class PathManager
{
    public List<Vector2> Waypoints { get; private set; }
    public Vector2 CitadelPosition { get; private set; }

    public PathManager(List<Vector2> waypoints, Vector2 citadelPosition)
    {
        Waypoints = waypoints ?? new List<Vector2>();
        CitadelPosition = citadelPosition;
    }

    public Vector2 GetNextWaypoint(int currentWaypointIndex)
    {
        if (currentWaypointIndex < Waypoints.Count)
        {
            return Waypoints[currentWaypointIndex];
        }
        return CitadelPosition; // Последняя цель — цитадель
    }

    public bool HasReachedCitadel(int currentWaypointIndex)
    {
        return currentWaypointIndex >= Waypoints.Count;
    }
}