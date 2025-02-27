using Microsoft.Xna.Framework;
using Tower.Core.Managers;
using Tower.Core.Models.Abstractions;
using Tower.Core.Models.Base;

namespace Tower.Core.Models;

public class BasicEnemy : BaseEnemy , IEnemy
{
    public BasicEnemy(int health, int speed, int reward, EnemyTypeEnum type) : base(health, speed, reward, type)
    {
    }

    public override void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public override bool IsDefeated() => Health <= 0;

    public override void Move(Vector2 targetPosition)
    {
        var direction = targetPosition - Position;

        // Проверка, чтобы не делить на 0
        if (direction.Length() > 0) 
        {
            direction = Vector2.Normalize(direction);
            Position += direction * Speed;
        }
    }

    public override void FollowPath(PathManager pathManager, Citadel citadel)
    {
      
        var target = pathManager.GetNextWaypoint(CurrentWaypointIndex);
        Move(target);

        if (Vector2.Distance(Position, target) < 5f)
        {
            CurrentWaypointIndex++;
            AttackCitadel(citadel);
        }
    }

    private void AttackCitadel(Citadel citadel)
    {
        citadel.TakeDamage(10);
    }
}