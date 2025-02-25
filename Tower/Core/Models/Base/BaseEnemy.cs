using Microsoft.Xna.Framework;
using Tower.Core.Managers;

namespace Tower.Core.Models.Base;

public abstract class BaseEnemy
{
    public int CurrentWaypointIndex{ get; set; }
    public Vector2 Position { get; set; }

    public int Health { get; set; }
    public int Speed { get; set; }
    public int Reward { get; set; }

    protected BaseEnemy(int health, int speed, int reward)
    {
        CurrentWaypointIndex = 0;
        Health = health;
        Speed = speed;
        Reward = reward;
    }

    public abstract void TakeDamage(int damage);
    public abstract bool IsDefeated();
    
    public abstract void Move(Vector2 targetPosition);
    
    public abstract void FollowPath(PathManager pathManager, Citadel citadel);
}