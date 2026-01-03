using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Managers;

namespace Tower.Renderers;

/// <summary>
/// Рендерер для состояния "Игра" (Playing). Отвечает за отрисовку всех игровых объектов
/// во время активной волны врагов.
/// </summary>
public static class PlayingRenderer
{
    /// <summary>
    /// Отрисовка игрового состояния "Игра". Отрисовывает тайлы земли, башни, снаряды,
    /// цитадель, врагов и HUD.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="graphicsDevice">Графическое устройство</param>
    /// <param name="gameTime">Время игры для синхронизации анимаций</param>
    public static void Render(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, GameTime gameTime)
    {
        // Отрисовываем тайлы земли на уровне 0 (самый дальний слой)
        TileRenderer.DrawGroundTiles(spriteBatch, graphicsDevice);
        
        // Отрисовываем все игровые объекты через GameEngine (автоматически отрисовывает объекты с IHaveDrawLogic)
        GameManager.GameEngine?.Draw(gameTime);
        
        // Отрисовка HUD (счет, деньги, номер волны)
        GameManager.UIManager.GameHUD.Draw(spriteBatch, gameTime);
    }
}

