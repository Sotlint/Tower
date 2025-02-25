using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Tower.Core.Models.Base;

namespace Tower.Core.Models;

public class Tower : BaseBuilding
{
    public Tower(int cost, int health, int attackPower, Vector2 position) : base(cost, health, position)
    {
        AttackPower = attackPower;
    }

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

    public override void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public override bool IsDestroyed() => Health <= 0;
}