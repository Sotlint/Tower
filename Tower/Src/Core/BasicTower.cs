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

public class BasicTower : ITower
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

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(
            Sprite, // текстура
            Position, // позиция
            null, // исходный прямоугольник (null = вся текстура)
            Color.White, // цвет (без изменений)
            0f, // поворот
            Vector2.Zero, // точка привязки (верхний левый угол)
            SpriteScale, // масштаб
            SpriteEffects.None, // эффекты (например, зеркальное отражение)
            0f // слой (глубина)
        );

        spriteBatch.End();
    }

    public void Update(GameTime gameTime)
    {
        TimeSinceLastAttack += gameTime.ElapsedGameTime;
        if (TimeSinceLastAttack < AttackDelay)
            return;

        var target = GameManager.EnemyManager.GetAliveEnemies()
            .Where(e => Vector2.Distance(Position, e.Position) <= AttackRange)
            .MinBy(e => Vector2.Distance(Position, e.Position));

        if (target != null)
        {
            Attack(new List<ICanDie>(new[] { target }));
        }

        TimeSinceLastAttack = TimeSpan.Zero;
    }

    public void Attack(IEnumerable<ICanDie> target)
    {
        var canDie = target.ToList();
        if (canDie.Any())
        {
            GameManager.ProjectileManager.AddProjectile(
                ProjectileFactory.CreateProjectile(ProjectileTypeEnum.Orb, Position, (IEnemy)canDie.First()));
        }
    }

    public void TakeDamage(int damage)
        => Health -= damage;

    public bool IsDie()
        => Health <= 0;

    public void SetPosition(Vector2 direction)
        => Position = direction;
}