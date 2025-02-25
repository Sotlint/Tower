using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Tower.Core.Models.Base;

namespace Tower.Core.Models;

public class Citadel : BaseBuilding
{
    public Citadel(int cost, int health, Vector2 position) : base(cost, health, position)
    {
    }

    public override void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public override bool IsDestroyed() => Health <= 0;

    public override void Attack(List<BaseEnemy> enemies)
    {
        var target = enemies
            .Where(e => Vector2.Distance(Position, e.Position) <= AttackRange)
            .MinBy(e => Vector2.Distance(Position, e.Position));
        
        if (target != null)
        {
            target.Health-= AttackPower;
        }
    }
}