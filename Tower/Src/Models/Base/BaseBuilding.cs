using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Tower.Src.Models.Abstractions;

namespace Tower.Src.Models.Base;

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
        AttackPower = 100;
        AttackRange = 50.0f;
    }

    public abstract void Attack(List<IEnemy> enemies);
    public abstract void TakeDamage(int damage);
    public abstract bool IsDestroyed();
}