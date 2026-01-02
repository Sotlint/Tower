using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.Renderers;

/// <summary>
/// Главный рендерер игры. Отвечает за отрисовку всех элементов игры
/// в зависимости от текущего состояния. Делегирует выполнение соответствующим рендерерам.
/// </summary>
public static class MainRenderer
{
    /// <summary>
    /// Главный метод отрисовки. Определяет текущее состояние игры
    /// и вызывает соответствующий рендерер для отрисовки.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="graphics">Менеджер графического устройства</param>
    /// <param name="gameTime">Время игры для синхронизации анимаций</param>
    public static void Render(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, GameTime gameTime)
    {
        // Получаем текущее состояние игры
        var state = GameManager.GameStateManager.CurrentState;
        
        // Очищаем экран белым цветом
        graphics.GraphicsDevice.Clear(Color.White);
        
        // Обработка состояния "Игра" - активная волна врагов
        if (state is GameStateEnum.Playing)
        {
            PlayingRenderer.Render(spriteBatch, graphics.GraphicsDevice, gameTime);
            return;
        }

        // Обработка состояния "Планирование" - размещение башен между волнами
        if (state is GameStateEnum.Planning)
        {
            PlanningRenderer.Render(spriteBatch, graphics.GraphicsDevice, gameTime);
            return;
        }

        // Обработка состояния "Главное меню"
        if (state is GameStateEnum.Menu)
        {
            MenuRenderer.Render(spriteBatch, gameTime);
            return;
        }

        // Обработка состояния "Пауза"
        if (state is GameStateEnum.Paused)
        {
            PausedRenderer.Render(spriteBatch, graphics.GraphicsDevice, gameTime);
            return;
        }

        // Обработка состояния "Игра окончена"
        if (state == GameStateEnum.GameOver)
        {
            GameOverRenderer.Render(spriteBatch, graphics.GraphicsDevice, gameTime);
            return;
        }
    }
}