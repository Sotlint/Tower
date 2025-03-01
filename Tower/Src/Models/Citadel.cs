using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Models.Abstractions;
using Tower.Models.Base;

namespace Tower.Models;

public class Citadel : BaseBuilding
{
    public Citadel(int cost, int health, Vector2 position) : base(cost, health, position)
    {
    }

    public override void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public override bool IsDestroyed() => Health <= 0;

    public override void Attack(List<IEnemy> enemies)
    {
        var target = enemies
            .Where(e => Vector2.Distance(Position, e.Position) <= AttackRange)
            .MinBy(e => Vector2.Distance(Position, e.Position));

        if (target != null)
        {
            target.TakeDamage(AttackPower);
            Console.WriteLine($"Цитадель атакует врага {target.Position} with {AttackPower} damage.");
        }
    }

    public override void Update(List<IEnemy> enemies)
    {
        Attack(enemies);
    }

    public override void Update()
    {
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        var rotation = 0f;
        var scale = 0.1f;          
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

    public void SetSprite(Texture2D sprite)
    {
        Sprite = sprite;
    }
}