using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Models.Abstractions;

namespace Tower.Models.Base;

public class BaseTower : BaseBuilding, IBuilding, ITower
{
    public Guid Id { get; }
    public Texture2D Sprite { get; }
    public TowerTypeEnum Type { get; set; }
    
    public BaseTower(int cost, int health, Vector2 position, TowerTypeEnum type, Texture2D sprite) : base(cost, health, position, sprite)
    {
        Id = Guid.NewGuid();
        Type = type;
    }

    public override void Update()
    {
        throw new NotImplementedException();
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

    public override void Update(List<IEnemy> enemies)
    {
        throw new NotImplementedException();
    }
}