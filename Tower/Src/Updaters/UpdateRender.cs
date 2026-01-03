using GameKit2D.Core;
using Microsoft.Xna.Framework;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.Updaters;

public static class UpdateRender
{
    public static void Draw(GameEngine engine, GameTime gameTime)
    {
        // Получаем текущее состояние игры
        var state = GameStateManager.CurrentState;

        // Обработка состояния "Игра" - активная волна врагов
        if (state is GameStateEnum.Playing)
        {
            engine.Update(gameTime);
        }

        // Обработка состояния "Планирование" - размещение башен между волнами
        if (state is GameStateEnum.Planning)
        {
            engine.Update(gameTime);
        }

        // Обработка состояния "Главное меню"
        if (state is GameStateEnum.Menu)
        {
            engine.Update(gameTime);
        }

        // Обработка состояния "Пауза"
        if (state is GameStateEnum.Paused)
        {
            engine.Update(gameTime);
        }

        // Обработка состояния "Игра окончена"
        if (state == GameStateEnum.GameOver)
        {
            engine.Update(gameTime);
        }
    }
}