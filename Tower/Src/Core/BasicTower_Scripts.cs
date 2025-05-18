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
    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        spriteBatch.Begin();
        if (GameManager.InputManager.IsDebugMode)
        {
            DebugBorderDrawer.DrawDebug(spriteBatch, Bounds, AttackRange, Position);
        }
        spriteBatch.Draw(
            Sprite, // текстура
            Position, // позиция
            null, // исходный прямоугольник (null = вся текстура)
            Color.White, // цвет (без изменений)
            0f, // поворот
            new Vector2(Sprite.Width/2, Sprite.Height/2), // точка привязки (верхний левый угол)
            SpriteScale, // масштаб
            SpriteEffects.None, // эффекты (например, зеркальное отражение)
            1f // слой (глубина)
        );
        spriteBatch.End();
    }

    public void Update(GameTime gameTime)
    {
        TimeSinceLastAttack += gameTime.ElapsedGameTime;
        if (TimeSinceLastAttack < AttackDelay)
            return;

        var target = GameManager.EnemyManager.GetEnemies()
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

    public void ResolveCollision()
    {
        throw new NotImplementedException();
    }

    public void UpdateBounds()
    {
        var width = (int)(Sprite.Width * SpriteScale);
        var height = (int)(Sprite.Height * SpriteScale);

        Bounds = new Rectangle(
            (int)(Position.X - width / 2), // Центрирование по X
            (int)(Position.Y - height / 2), // Центрирование по Y
            width,
            height
        );
    }
}