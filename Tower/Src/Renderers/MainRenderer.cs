using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.Renderers;

public static class MainRenderer
{
    public static void Render(GameManager gameManager, SpriteBatch spriteBatch, GraphicsDeviceManager graphics, GameTime gameTime)
    {
        var state = gameManager.GameStateManager.CurrentState;
        graphics.GraphicsDevice.Clear(Color.White);
        if (state is GameStateEnum.Playing)
        {
            foreach (var tower in gameManager.TowerManager.GetTowers())
            {
                tower.Draw(spriteBatch, gameTime);
            }
            
            gameManager.CitadelManager.GetCitadel().Draw(spriteBatch, gameTime);
            var enemies = gameManager.EnemyManager.GetEnemies();
            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch, gameTime);
            }
            return;
        }
        if (state is GameStateEnum.Planning)
        {
            graphics.GraphicsDevice.Clear(Color.Green);
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