using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Managers;

namespace Tower.Renderers;

/// <summary>
/// Рендерер для состояния "Игра окончена" (GameOver). Отвечает за отрисовку последнего кадра игры
/// с меню окончания игры поверх.
/// </summary>
public static class GameOverRenderer
{
    /// <summary>
    /// Отрисовка игрового состояния "Игра окончена". Отрисовывает последний кадр игры
    /// под меню (финальное состояние) и меню окончания игры поверх.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="graphicsDevice">Графическое устройство</param>
    /// <param name="gameTime">Время игры для синхронизации анимаций</param>
    public static void Render(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, GameTime gameTime)
    {
        // Отрисовываем тайлы земли на уровне 0 (самый дальний слой)
        TileRenderer.DrawGroundTiles(spriteBatch, graphicsDevice);
        
        // Отрисовываем последний кадр игры под меню (финальное состояние) через GameEngine
        GameManager.GameEngine?.Draw(gameTime);
        
        // Отрисовываем меню окончания игры поверх (полупрозрачное)
        var menu = GameManager.UIManager.GetActiveMenu();
        menu?.Draw(spriteBatch, gameTime);
    }
}

