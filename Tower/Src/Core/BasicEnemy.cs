using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Enums;
using Tower.Core.Helpers;

namespace Tower.Core;

public partial class BasicEnemy : IEnemy
{
    public BasicEnemy(Texture2D sprite, Guid id, Vector2 position, int health, int speed, int attackPower,
        float attackRange, EnemyTypeEnum type, TimeSpan attackDelay, HealthBar healthBar)
    {
        Sprite = sprite;
        Id = id;
        Position = position;
        Health = health;
        MaxHealth = health;
        Speed = speed;
        AttackPower = attackPower;
        AttackRange = attackRange;
        Type = type;
        TimeSinceLastAttack = TimeSpan.Zero;
        AttackDelay = attackDelay;
        SpriteScale = this.GetSpriteScale();
        HealthBar = healthBar;
        Reward = 10;
        UpdateBounds();
    }

    public float SpriteScale { get; init; }
    public Texture2D Sprite { get; private set; }
    public EnemyTypeEnum Type { get; private set; }
    public int Reward { get; init; }
    public Guid Id { get; private set; }
    public Vector2 Position { get; private set; }
    public int MaxHealth { get; init; }
    public int Health { get; private set; }
    public int Speed { get; private set; }
    public int AttackPower { get; private set; }
    public float AttackRange { get; private set; }
    public TimeSpan AttackDelay { get; private set; }
    private HealthBar HealthBar { get; init; }
    public TimeSpan TimeSinceLastAttack { get; private set; }
    public Rectangle Bounds { get; private set; }
}