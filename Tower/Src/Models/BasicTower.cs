using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Models.Abstractions;
using Tower.Models.Base;

namespace Tower.Models;

public class BasicTower : BaseTower, IBuilding, ITower
{
    public BasicTower(int cost, int health, int attackPower, Vector2 position, TowerTypeEnum type, Texture2D sprite) :
        base(cost, health,
            position, type, sprite)
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
    public override void Update()
    {
    }
    
    public override void Update(List<IEnemy> enemies)
    {
        Attack(enemies);
    }
    
    public override void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public override bool IsDestroyed() => Health <= 0;
}