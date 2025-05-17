using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Base;

namespace Tower.Core;

public partial class OrbProjectile : IProjectile
{
    public OrbProjectile(Vector2 position, int attackPower, float attackRange, TimeSpan attackDelay, int speed,
        float spriteScale, Texture2D sprite, IEnemy target)
    {
        Position = position;
        AttackPower = attackPower;
        AttackRange = attackRange;
        AttackDelay = attackDelay;
        TimeSinceLastAttack = TimeSpan.Zero;
        Speed = speed;
        SpriteScale = spriteScale;
        Sprite = sprite;
        Target = target;
        IsAttacked = false;
    }

    public IEnemy Target { get; init; }
    public bool IsAttacked { get; private set; }
    public Vector2 Position { get; private set; }
    public int AttackPower { get; init; }
    public float AttackRange { get; init; }
    public TimeSpan AttackDelay { get; init; }
    public TimeSpan TimeSinceLastAttack { get; set; }
    public int Speed { get; init; }
    public float SpriteScale { get; init; }
    public Texture2D Sprite { get; init; }
}