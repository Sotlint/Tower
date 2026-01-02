using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.Renderers;

/// <summary>
/// Главный рендерер игры. Отвечает за отрисовку всех элементов игры
/// в зависимости от текущего состояния.
/// </summary>
public static class MainRenderer
{
    /// <summary>
    /// Главный метод отрисовки. Определяет текущее состояние игры
    /// и вызывает соответствующие методы отрисовки.
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
            // Отрисовываем тайлы земли на уровне 0 (самый дальний слой)
            TileRenderer.DrawGroundTiles(spriteBatch, graphics.GraphicsDevice);
            
            // Отрисовываем все игровые объекты
            GameManager.TowerManager.DrawTowers(spriteBatch, gameTime); // Башни (уровень 1)
            GameManager.ProjectileManager.DrawProjectiles(spriteBatch, gameTime); // Снаряды (уровень 2)
            GameManager.CitadelManager.Draw(spriteBatch, gameTime); // Цитадель (уровень 1)
            GameManager.EnemyManager.DrawEnemies(spriteBatch, gameTime); // Враги
            
            // Отрисовка HUD (счет, деньги, номер волны)
            GameManager.UIManager.GameHUD.Draw(spriteBatch, gameTime);
            return;
        }

        // Обработка состояния "Планирование" - размещение башен между волнами
        if (state is GameStateEnum.Planning)
        {
            // Отрисовываем тайлы земли на уровне 0 (самый дальний слой)
            TileRenderer.DrawGroundTiles(spriteBatch, graphics.GraphicsDevice);
            
            // Отрисовываем игровое поле (башни и цитадель)
            GameManager.TowerManager.DrawTowers(spriteBatch, gameTime); // Башни (уровень 1)
            GameManager.CitadelManager.Draw(spriteBatch, gameTime); // Цитадель (уровень 1)
            
            // Отрисовка радиуса атаки для башни, на которую наведена мышь
            LogicUpdaters.PlannedUpdateLogic.DrawHoveredTowerRange(spriteBatch, gameTime);
            
            // Отрисовка перетаскиваемой башни (следует за курсором) и её радиуса атаки
            LogicUpdaters.PlannedUpdateLogic.DrawDraggedTower(spriteBatch, gameTime);
            
            // Отрисовка UI элементов фазы планирования
            GameManager.UIManager.TowerSelectionMenu.Draw(spriteBatch); // Меню выбора башен
            GameManager.UIManager.DrawStartWaveButton(spriteBatch); // Кнопка начала волны
            
            // Отрисовка HUD (счет, деньги, номер волны)
            GameManager.UIManager.GameHUD.Draw(spriteBatch, gameTime);
            return;
        }

        // Обработка состояния "Главное меню"
        if (state is GameStateEnum.Menu)
        {
            var menu = GameManager.UIManager.GetActiveMenu();
            // Отрисовываем только меню (игровое поле не видно)
            menu?.Draw(spriteBatch, gameTime);
            return;
        }

        // Обработка состояния "Пауза"
        if (state is GameStateEnum.Paused)
        {
            // Отрисовываем тайлы земли на уровне 0 (самый дальний слой)
            TileRenderer.DrawGroundTiles(spriteBatch, graphics.GraphicsDevice);
            
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
            return;
        }

        // Обработка состояния "Игра окончена"
        if (state == GameStateEnum.GameOver)
        {
            // Отрисовываем тайлы земли на уровне 0 (самый дальний слой)
            TileRenderer.DrawGroundTiles(spriteBatch, graphics.GraphicsDevice);
            
            // Отрисовываем последний кадр игры под меню (финальное состояние)
            GameManager.TowerManager.DrawTowers(spriteBatch, gameTime); // Башни (уровень 1)
            GameManager.ProjectileManager.DrawProjectiles(spriteBatch, gameTime); // Снаряды (уровень 2)
            GameManager.CitadelManager.Draw(spriteBatch, gameTime); // Цитадель (уровень 1)
            GameManager.EnemyManager.DrawEnemies(spriteBatch, gameTime); // Враги
            
            // Отрисовываем меню окончания игры поверх (полупрозрачное)
            var menu = GameManager.UIManager.GetActiveMenu();
            menu?.Draw(spriteBatch, gameTime);
            return;
        }
    }
}