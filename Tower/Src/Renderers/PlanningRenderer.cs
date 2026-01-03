using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.LogicUpdaters;
using Tower.Managers;

namespace Tower.Renderers;

/// <summary>
/// Рендерер для состояния "Планирование" (Planning). Отвечает за отрисовку игрового поля
/// во время фазы планирования, когда игрок размещает башни.
/// </summary>
public static class PlanningRenderer
{
    /// <summary>
    /// Отрисовка игрового состояния "Планирование". Отрисовывает тайлы земли, башни, цитадель,
    /// радиусы атаки, перетаскиваемую башню, меню выбора башен и HUD.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="graphicsDevice">Графическое устройство</param>
    /// <param name="gameTime">Время игры для синхронизации анимаций</param>
    public static void Render(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, GameTime gameTime)
    {
        // Отрисовываем тайлы земли на уровне 0 (самый дальний слой)
        TileRenderer.DrawGroundTiles(spriteBatch, graphicsDevice);
        
        // Отрисовываем игровое поле (башни и цитадель) через GameEngine
        GameManager.GameEngine?.Draw(gameTime);
        
        // Отрисовка радиуса атаки для башни, на которую наведена мышь
        PlannedUpdateLogic.DrawHoveredTowerRange(spriteBatch, gameTime);
        
        // Отрисовка перетаскиваемой башни (следует за курсором) и её радиуса атаки
        PlannedUpdateLogic.DrawDraggedTower(spriteBatch, gameTime);
        
        // Отрисовка точек спавна врагов с пульсирующим эффектом и количеством врагов
        SpawnPointRenderer.DrawSpawnPoints(spriteBatch, gameTime);
        
        // Отрисовка UI элементов фазы планирования
        GameManager.UIManager.TowerSelectionMenu.Draw(spriteBatch); // Меню выбора башен
        GameManager.UIManager.DrawStartWaveButton(spriteBatch); // Кнопка начала волны
        
        // Отрисовка HUD (счет, деньги, номер волны)
        GameManager.UIManager.GameHUD.Draw(spriteBatch, gameTime);
    }
}

