using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Models.Abstractions;

namespace Tower.Models.Base;

public class BaseTower : BaseBuilding, IBuilding, ITower
{
    public Guid Id { get; }
    public Texture2D Sprite { get; }
    public TowerTypeEnum Type { get; set; }
    
    public BaseTower(int cost, int health, Vector2 position, TowerTypeEnum type, Texture2D sprite) : base(cost, health, position)
    {
        Id = Guid.NewGuid();
        Type = type;
        Sprite = sprite;
    }

    public override void Update()
    {
        throw new NotImplementedException();
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

    public override void Attack(List<IEnemy> enemies)
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    public override bool IsDestroyed()
    {
        throw new System.NotImplementedException();
    }

    public override void Update(List<IEnemy> enemies)
    {
        throw new NotImplementedException();
    }
}