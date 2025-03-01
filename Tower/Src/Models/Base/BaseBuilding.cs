using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Models.Abstractions;

namespace Tower.Models.Base;

public abstract class BaseBuilding : IBuilding
{
    public Guid Id { get; set; }
    public Texture2D Sprite { get; set; }
    public Vector2 Position { get; set; }
    public int AttackPower { get; set; }
    public float AttackRange { get; set; }
    public int Cost { get; set; }
    public int Health { get; set; }

    protected BaseBuilding(int cost, int health, Vector2 position, Texture2D sprite)
    {
        Id = Guid.NewGuid();
        Cost = cost;
        Health = health;
        Position = position;
        AttackPower = 100;
        AttackRange = 200.0f;
        Sprite = sprite;
    }

    public abstract void Attack(List<IEnemy> enemies);
    public abstract void TakeDamage(int damage);
    public abstract bool IsDestroyed();

    public abstract void Update(List<IEnemy> enemies);
    
    public abstract void Update();
}