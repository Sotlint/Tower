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
            GameManager.UIManager.TowerSelectionMenu.Draw(spriteBatch);
            return;
        }

        if (state is GameStateEnum.Menu)
        {
            graphics.GraphicsDevice.Clear(Color.Black);
            return;
        }

        if (state is GameStateEnum.Paused)
        {
            graphics.GraphicsDevice.Clear(Color.Gold);
            return;
        }

        if (state == GameStateEnum.GameOver)
        {
            graphics.GraphicsDevice.Clear(Color.Red);
            return;
        }
    }
}