using GameKit2D.Core;
using Microsoft.Xna.Framework;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.Updaters;

/// <summary>
/// Основная логика обновления игры. Управляет обновлением различных состояний игры
/// и делегирует выполнение соответствующим логикам обновления.
/// </summary>
public static class UpdateLogic
{
    /// <summary>
    /// Главный метод обновления игры. Вызывается каждый кадр.
    /// Определяет текущее состояние игры и вызывает соответствующую логику обновления.
    /// </summary>
    /// <param name="gameTime">Время игры, используется для синхронизации обновлений</param>
    public static void Update(GameEngine engine, GameTime gameTime)
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