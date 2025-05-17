using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Base;
using Tower.Managers;

namespace Tower.Core;

public partial class BasicEnemy : IEnemy
{
    public void TakeDamage(int damage)
        => Health -= damage;

    public bool IsDie() => Health <= 0;

    public void Move(Vector2 targetPosition)
    {
        var direction = targetPosition - Position;

        // Проверка, чтобы не делить на 0
        if (direction.Length() > 0)
        {
            direction = Vector2.Normalize(direction);
            SetPosition(direction * Speed);
        }
    }

    public void Update(GameTime gameTime)
    {
        var citadel = GameManager.CitadelManager.GetCitadel();

        if (IsDie())
            return;

        ResolveCollision();
        if (Vector2.Distance(Position, citadel.Position) <= AttackRange)
        {
            TimeSinceLastAttack += gameTime.ElapsedGameTime;
            if (TimeSinceLastAttack < AttackDelay)
                return;

            Attack(new List<ICanDie>() { citadel });
            TimeSinceLastAttack = TimeSpan.Zero;

            return;
        }

        Move(citadel.Position);
        TimeSinceLastAttack += gameTime.ElapsedGameTime;
    }

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
            1f // слой (глубина)
        );
        HealthBar.Draw(
            spriteBatch,
            gameTime,
            this
        );
        spriteBatch.End();
    }

    public void Attack(IEnumerable<ICanDie> targets)
    {
        foreach (var target in targets)
        {
            target.TakeDamage(AttackPower);
        }
    }

    public void ResolveCollision()
    {
        var enemies = GameManager.EnemyManager.GetEnemies().Where(x => x.Id != Id);
        var thisBounds = GetBounds();

        foreach (var enemy in enemies)
        {
            var enemyBounds = enemy.GetBounds();
            // Проверяем пересечение
            if (thisBounds.Intersects(enemyBounds))
            {
                // Вычисляем направление, чтобы раздвинуть врагов
                var moveDirection = Position - enemy.Position;
                // Если враги находятся на одном месте, создаем случайное направление
                if (moveDirection == Vector2.Zero)
                {
                    moveDirection = new Vector2(
                        (float)(0.5 - Random.Shared.NextDouble()),
                        (float)(0.5 - Random.Shared.NextDouble())
                    );
                }

                moveDirection = Vector2.Normalize(moveDirection);
                // Сдвигаем врагов в разные стороны
                var overlap = (thisBounds.Width / 2f + enemyBounds.Width / 2f) -
                              Vector2.Distance(Position, enemy.Position);
                Position += moveDirection * (overlap / 2);

                SetPosition(moveDirection * (overlap / 2));
            }
        }
    }

    public Rectangle GetBounds()
    {
        var width = (int)(Sprite.Width * SpriteScale);
        var height = (int)(Sprite.Height * SpriteScale);

        return new Rectangle(
            (int)(Position.X - width / 2), // Центрирование по X
            (int)(Position.Y - height / 2), // Центрирование по Y
            width,
            height
        );
    }


    public void SetPosition(Vector2 direction)
        => Position += direction;
}