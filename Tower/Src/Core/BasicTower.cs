using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Base;
using Tower.Core.Abstractions.Enums;
using Tower.Core.Helpers;
using Tower.Factories;
using Tower.Managers;

namespace Tower.Core;

public partial class BasicTower : ITower
{
    public BasicTower(Guid id, Texture2D sprite, Vector2 position, TowerTypeEnum type, int health, int attackPower,
        float attackRange, TimeSpan attackDelay)
    {
        Id = id;
        Sprite = sprite;
        Position = position;
        Type = type;
        Health = health;
        MaxHealth = health;
        AttackPower = attackPower;
        AttackRange = attackRange;
        TimeSinceLastAttack = TimeSpan.Zero;
        AttackDelay = attackDelay;
        SpriteScale = this.GetSpriteScale();
    }

    public Guid Id { get; private set; }
    public float SpriteScale { get; init; }
    public Texture2D Sprite { get; private set; }
    public Vector2 Position { get; private set; }
    public TowerTypeEnum Type { get; private set; }
    public int MaxHealth { get; init; }
    public int Health { get; private set; }
    public int AttackPower { get; private set; }
    public float AttackRange { get; private set; }
    public TimeSpan AttackDelay { get; private set; }
    public TimeSpan TimeSinceLastAttack { get; private set; }
}