using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Tower.Core.Models.Abstractions;

namespace Tower.Core.Models.Base;

public abstract class BaseBuilding : IBuilding
{
    public Vector2 Position { get; set; }
    public int AttackPower { get; set; }
    public float AttackRange { get; set; }
    public int Cost { get; set; }
    public int Health { get; set; }

    protected BaseBuilding(int cost, int health, Vector2 position)
    {
        Cost = cost;
        Health = health;
        Position = position;
    }

    public abstract void Attack(List<BaseEnemy> enemies);
    public abstract void TakeDamage(int damage);
    public abstract bool IsDestroyed();
}