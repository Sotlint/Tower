using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.Renderers;

public static class MainRenderer
{
    public static void Render(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, GameTime gameTime)
    {
        var state = GameManager.GameStateManager.CurrentState;
        graphics.GraphicsDevice.Clear(Color.White);
        if (state is GameStateEnum.Playing)
        {
            GameManager.TowerManager.DrawTowers(spriteBatch, gameTime);
            GameManager.ProjectileManager.DrawProjectiles(spriteBatch, gameTime);
            GameManager.CitadelManager.Draw(spriteBatch, gameTime);
            GameManager.EnemyManager.DrawEnemies(spriteBatch, gameTime);
            return;
        }

        if (state is GameStateEnum.Planning)
        {
            GameManager.TowerManager.DrawTowers(spriteBatch, gameTime);
            GameManager.CitadelManager.Draw(spriteBatch, gameTime);
            
            // Отрисовка перетаскиваемой башни
            LogicUpdaters.PlannedUpdateLogic.DrawDraggedTower(spriteBatch, gameTime);
            
            // Отрисовка UI
            GameManager.UIManager.TowerSelectionMenu.Draw(spriteBatch);
            GameManager.UIManager.DrawStartWaveButton(spriteBatch);
            return;
        }

        if (state is GameStateEnum.Menu)
        {
            var menu = GameManager.UIManager.GetActiveMenu();
            menu?.Draw(spriteBatch, gameTime);
            return;
        }

        if (state is GameStateEnum.Paused)
        {
            // Отрисовываем игру под меню паузы
            GameManager.TowerManager.DrawTowers(spriteBatch, gameTime);
            GameManager.ProjectileManager.DrawProjectiles(spriteBatch, gameTime);
            GameManager.CitadelManager.Draw(spriteBatch, gameTime);
            GameManager.EnemyManager.DrawEnemies(spriteBatch, gameTime);
            
            // Отрисовываем меню паузы поверх
            var menu = GameManager.UIManager.GetActiveMenu();
            menu?.Draw(spriteBatch, gameTime);
            return;
        }

        if (state == GameStateEnum.GameOver)
        {
            // Отрисовываем последний кадр игры под меню
            GameManager.TowerManager.DrawTowers(spriteBatch, gameTime);
            GameManager.ProjectileManager.DrawProjectiles(spriteBatch, gameTime);
            GameManager.CitadelManager.Draw(spriteBatch, gameTime);
            GameManager.EnemyManager.DrawEnemies(spriteBatch, gameTime);
            
            // Отрисовываем меню окончания игры поверх
            var menu = GameManager.UIManager.GetActiveMenu();
            menu?.Draw(spriteBatch, gameTime);
            return;
        }
    }
}