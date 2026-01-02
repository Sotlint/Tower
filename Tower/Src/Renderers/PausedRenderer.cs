using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Managers;

namespace Tower.Renderers;

/// <summary>
/// Рендерер для состояния "Пауза" (Paused). Отвечает за отрисовку замороженного состояния игры
/// с меню паузы поверх.
/// </summary>
public static class PausedRenderer
{
    /// <summary>
    /// Отрисовка игрового состояния "Пауза". Отрисовывает игру под меню паузы
    /// (замороженное состояние) и меню паузы поверх.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="graphicsDevice">Графическое устройство</param>
    /// <param name="gameTime">Время игры для синхронизации анимаций</param>
    public static void Render(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, GameTime gameTime)
    {
        // Отрисовываем тайлы земли на уровне 0 (самый дальний слой)
        TileRenderer.DrawGroundTiles(spriteBatch, graphicsDevice);
        
        // Отрисовываем игру под меню паузы (замороженное состояние)
        GameManager.TowerManager.DrawTowers(spriteBatch, gameTime); // Башни (уровень 1)
        GameManager.ProjectileManager.DrawProjectiles(spriteBatch, gameTime); // Снаряды (уровень 2)
        GameManager.CitadelManager.Draw(spriteBatch, gameTime); // Цитадель (уровень 1)
        GameManager.EnemyManager.DrawEnemies(spriteBatch, gameTime); // Враги
        
        // Отрисовка HUD (счет, деньги, номер волны)
        GameManager.UIManager.GameHUD.Draw(spriteBatch, gameTime);
        
        // Отрисовываем меню паузы поверх игры (полупрозрачное)
        var menu = GameManager.UIManager.GetActiveMenu();
        menu?.Draw(spriteBatch, gameTime);
    }
}

