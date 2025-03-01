using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Models.Abstractions;
using Tower.Models.Base;

namespace Tower.Models;

public class BasicEnemy : BaseEnemy, IEnemy
{
    public BasicEnemy(int health, int speed, int reward, EnemyTypeEnum type, Texture2D sprite) : base(health, speed, reward, type, sprite)
    {
    }

    public override void TakeDamage(int damage)
        => Health -= damage;

    public override bool IsDefeated() => Health <= 0;

    public override void Move(Vector2 targetPosition)
    {
        var direction = targetPosition - Position;

        // Проверка, чтобы не делить на 0
        if (direction.Length() > 0)
        {
            direction = Vector2.Normalize(direction);
            Position += direction * Speed;
        }
    }

    public override void Update(Citadel citadel)
    {
        Move(citadel.Position);
        Console.WriteLine($"Враг {Id} движется в направлении {Position}");
        if (Vector2.Distance(Position, citadel.Position) < 5f)
        {
            CurrentWaypointIndex++;
            AttackCitadel(citadel);
            Console.WriteLine($"Враг {Id} ударил цитадель. У цитадели осталось {citadel.Health}");
        }
    }
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        var rotation = 0f;
        var scale = 0.05f;          // 20% от оригинального размера
        spriteBatch.Draw(
            Sprite,                 // текстура
            Position,               // позиция
            null,      // исходный прямоугольник (null = вся текстура)
            Color.White,            // цвет (без изменений)
            rotation,               // поворот
            Vector2.Zero,      // точка привязки (верхний левый угол)
            scale,                  // масштаб
            SpriteEffects.None,     // эффекты (например, зеркальное отражение)
            0f             // слой (глубина)
        );
        spriteBatch.End();
    }

    private void AttackCitadel(Citadel citadel)
    {
        citadel.TakeDamage(10);
    }
}