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
            foreach (var tower in GameManager.TowerManager.GetTowers())
            {
                tower.Draw(spriteBatch, gameTime);
            }

            GameManager.ProjectileManager.DrawProjectiles(spriteBatch, gameTime);
            GameManager.CitadelManager.GetCitadel().Draw(spriteBatch, gameTime);
            var enemies = GameManager.EnemyManager.GetAliveEnemies();
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