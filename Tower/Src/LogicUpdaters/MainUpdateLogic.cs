using Microsoft.Xna.Framework;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.LogicUpdaters;

/// <summary>
/// Основная логика обновления игры. Управляет обновлением различных состояний игры
/// и делегирует выполнение соответствующим логикам обновления.
/// </summary>
public static class MainUpdateLogic
{
    /// <summary>
    /// Главный метод обновления игры. Вызывается каждый кадр.
    /// Определяет текущее состояние игры и вызывает соответствующую логику обновления.
    /// </summary>
    /// <param name="gameTime">Время игры, используется для синхронизации обновлений</param>
    public static void Update(GameTime gameTime)
    {
        // Получаем текущее состояние игры
        var state = GameManager.GameStateManager.CurrentState;
        
        // Обновляем менеджер ввода (клавиатура, мышь)
        GameManager.InputManager.Update(gameTime);
        
        // Обработка состояния "Игра" - активная волна врагов
        if (state is GameStateEnum.Playing)
        {
            WaveUpdateLogic.Update(gameTime);
            return;
        }
        
        // Обработка состояния "Планирование" - размещение башен между волнами
        if (state is GameStateEnum.Planning)
        {
            PlannedUpdateLogic.Update(gameTime);
            return;
        }
        
        // Обработка состояния "Главное меню"
        if (state is GameStateEnum.Menu)
        {
            var menu = GameManager.UIManager.GetActiveMenu();
            // Обновляем состояние меню (hover эффекты и т.д.)
            menu?.Update(gameTime, GameManager.InputManager.MousePosition);
            
            // Обработка кликов по меню
            if (GameManager.InputManager.LeftClick)
            {
                menu?.HandleClick(GameManager.InputManager.MousePosition);
            }
            return;
        }
        
        // Обработка состояния "Пауза"
        if (state is GameStateEnum.Paused)
        {
            var menu = GameManager.UIManager.GetActiveMenu();
            // Обновляем меню паузы
            menu?.Update(gameTime, GameManager.InputManager.MousePosition);
            
            // Обработка кликов по меню паузы
            if (GameManager.InputManager.LeftClick)
            {
                menu?.HandleClick(GameManager.InputManager.MousePosition);
            }
            return;
        }
        
        // Обработка состояния "Игра окончена"
        if (state == GameStateEnum.GameOver)
        {
            var menu = GameManager.UIManager.GetActiveMenu();
            // Обновляем меню окончания игры
            menu?.Update(gameTime, GameManager.InputManager.MousePosition);
            
            // Обработка кликов по меню окончания игры
            if (GameManager.InputManager.LeftClick)
            {
                menu?.HandleClick(GameManager.InputManager.MousePosition);
            }
            return;
        }
    }
}