using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;
using Tower.Core.Helpers;

namespace Tower.Core;

public partial class Citadel : IBuilding
{
    public Citadel(Guid id, Vector2 position, int health, int attackPower, float attackRange, TimeSpan attackDelay,
        HealthBar healthBar)
    {
        Id = id;
        Position = position;
        Health = health;
        AttackPower = attackPower;
        AttackRange = attackRange;
        TimeSinceLastAttack = TimeSpan.Zero;
        AttackDelay = attackDelay;
        SpriteScale = this.GetSpriteScale();
        MaxHealth = health;
        HealthBar = healthBar;
    }
    private HealthBar HealthBar { get; init; }
    public Guid Id { get; private set; }
    public float SpriteScale { get; init; }
    public Texture2D Sprite { get; private set; }
    public Vector2 Position { get; private set; }
    public int MaxHealth { get; init; }
    public int Health { get; private set; }
    public int AttackPower { get; private set; }
    public float AttackRange { get; private set; }
    public TimeSpan AttackDelay { get; private set; }
    public TimeSpan TimeSinceLastAttack { get; private set; }
    public Rectangle Bounds { get; private set; }
}