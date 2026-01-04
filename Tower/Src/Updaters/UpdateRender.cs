#nullable enable
using GameKit2D.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.Updaters;

public static class UpdateRender
{

    public static void Draw(GameEngine engine, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, GameTime gameTime)
    {
        // Получаем текущее состояние игры
        var state = GameStateManager.CurrentState;

        // Обработка состояния "Главное меню"
        if (state is GameStateEnum.Menu)
        {
        }

        // Обработка состояния "Игра" - активная волна врагов
        if (state is GameStateEnum.Playing)
        {
            engine.Draw(gameTime);
        }

        // Обработка состояния "Планирование" - размещение башен между волнами
        if (state is GameStateEnum.Planning)
        {
          
        }

        // Обработка состояния "Пауза"
        if (state is GameStateEnum.Paused)
        {
            engine.Draw(gameTime);
        }

        // Обработка состояния "Игра окончена"
        if (state == GameStateEnum.GameOver)
        {
            engine.Draw(gameTime);
        }
    }
}