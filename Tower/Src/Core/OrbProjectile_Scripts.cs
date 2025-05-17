using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions.Base;

namespace Tower.Core;

public partial class OrbProjectile : IProjectile
{
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

    public void SetPosition(Vector2 direction)
    {
        Position += direction;
    }

    public void Attack(IEnumerable<ICanDie> targets)
    {
        foreach (var target in targets)
        {
                target?.TakeDamage(AttackPower);
        }
        IsAttacked = true;
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        if (!Target.IsDie())
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
            spriteBatch.End();
        }
    }

    public void Update(GameTime gameTime)
    {
        if (!Target.IsDie() && !IsAttacked)
        {
            if (Vector2.Distance(Position, Target.Position) <= AttackRange)
            {
                Attack(new List<ICanDie>(new[] { Target }));
            }
            else
            {
                Move(Target.Position);
            }
        }
    }
}