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
    
    /// <summary>
    /// Восстанавливает здоровье до максимума
    /// </summary>
    public void RestoreHealth()
    {
        Health = MaxHealth;
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
        spriteBatch.DrawSprite(Sprite, Position, scale);
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
            // Устанавливаем статистику перед переходом в GameOver
            var score = GameManager.GetScore();
            var wave = GameManager.DifficultyManager.GetDifficultyLevel();
            GameManager.UIManager.GameOverMenu.SetGameStats(score, wave);
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