using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Base;
using Tower.Core.Abstractions.Enums;
using Tower.Core.Helpers;
using Tower.Managers;

namespace Tower.Core;

public partial class Citadel : IBuilding
{
    public void Attack(IEnumerable<ICanDie> targets)
    {
        throw new NotImplementedException();
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public bool IsDie()
        => Health <= 0;

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        spriteBatch.Begin();
        var scale = 0.1f;
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
            scale, // масштаб
            SpriteEffects.None, // эффекты (например, зеркальное отражение)
            1f // слой (глубина)
        );
        HealthBar.Draw(spriteBatch, gameTime, this);
        spriteBatch.End();
    }

    public void Attack(ICanDie target)
    {
        throw new NotImplementedException();
    }

    public void Update( GameTime gameTime)
    {
        if (IsDie())
        {
            GameManager.GameStateManager.ChangeState(GameStateEnum.GameOver);
        }
    }

    public void SetPosition(Vector2 direction)
        => Position = direction;

    public void ResolveCollision()
    {
        throw new NotImplementedException();
    }

    public void UpdateBounds()
    {
        var width = (int)(Sprite.Width * 0.1f);
        var height = (int)(Sprite.Height * 0.1f);

        Bounds = new Rectangle(
            (int)(Position.X - width / 2), // Центрирование по X
            (int)(Position.Y - height / 2), // Центрирование по Y
            width,
            height
        );
    }
}