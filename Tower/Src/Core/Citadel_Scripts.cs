using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Base;
using Tower.Core.Abstractions.Enums;
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
        var rotation = 0f;
        var scale = 0.1f;
        spriteBatch.Draw(Sprite, Bounds, Color.White);
        HealthBar.Draw(spriteBatch, gameTime, this);
        spriteBatch.End();
    }

    public void SetSprite(Texture2D sprite)
    {
        Sprite = sprite;
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
        var width = (int)64;
        var height = (int)64;

        Bounds = new Rectangle(
            (int)(Position.X - width / 2), // Центрирование по X
            (int)(Position.Y - height / 2), // Центрирование по Y
            width,
            height
        );
    }
}