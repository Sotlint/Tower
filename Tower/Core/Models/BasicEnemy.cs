using Microsoft.Xna.Framework;
using Tower.Core.Managers;
using Tower.Core.Models.Base;

namespace Tower.Core.Models;

public class BasicEnemy : BaseEnemy
{
    public BasicEnemy(int health, int speed, int reward) : base(health, speed, reward) { }

    public override void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public override bool IsDefeated() => Health <= 0;
    
    public override void Move(Vector2 targetPosition)
    {
        var direction = targetPosition - Position;
    
        if (direction.Length() > 0) // Проверка, чтобы не делить на 0
        {
            direction = Vector2.Normalize(direction);
            Position += direction * Speed;
        }
    }

    public override void FollowPath(PathManager pathManager, Citadel citadel)
    {
        if (pathManager.HasReachedCitadel(CurrentWaypointIndex))
        {
            AttackCitadel(citadel);
            return;
        }

        var target = pathManager.GetNextWaypoint(CurrentWaypointIndex);
        Move(target);

        if (Vector2.Distance(Position, target) < 5f)
        {
            CurrentWaypointIndex++;
        }
    }
    
    private void AttackCitadel(Citadel citadel)
    {
        citadel.TakeDamage(10);
    }
}