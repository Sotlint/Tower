using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Tower.Src.Models.Abstractions;
using Tower.Src.Models.Base;

namespace Tower.Src.Models;

public class BasicTower : BaseTower, IBuilding, ITower
{
    public BasicTower(int cost, int health, int attackPower, Vector2 position, TowerTypeEnum type) : base(cost, health,
        position, type)
    {
        AttackPower = attackPower;
    }

    public override void Attack(List<IEnemy> enemies)
    {
        var target = enemies
            .Where(e => Vector2.Distance(Position, e.Position) <= AttackRange)
            .MinBy(e => Vector2.Distance(Position, e.Position));

        if (target != null)
        {
            target.TakeDamage(AttackPower);
            Console.WriteLine($"Tower attacked enemy at {target.Position} with {AttackPower} damage.");
        }
    }

    public override void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public override bool IsDestroyed() => Health <= 0;
}