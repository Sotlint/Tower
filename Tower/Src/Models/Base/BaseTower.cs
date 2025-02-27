using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Tower.Src.Models.Abstractions;

namespace Tower.Src.Models.Base;

public class BaseTower : BaseBuilding, IBuilding, ITower
{
    public Guid Id { get; }
    public TowerTypeEnum Type { get; set; }

    public BaseTower(int cost, int health, Vector2 position, TowerTypeEnum type) : base(cost, health, position)
    {
        Id = Guid.NewGuid();
        Type = type;
    }

    public override void Attack(List<IEnemy> enemies)
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    public override bool IsDestroyed()
    {
        throw new System.NotImplementedException();
    }

 
}