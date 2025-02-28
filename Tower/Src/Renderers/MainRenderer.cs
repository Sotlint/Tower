using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Managers;
using Tower.Models.Abstractions;

namespace Tower.Renderers;

public static class MainRenderer
{
    public static void Render(GameManager gameManager, SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
    {
        var state = gameManager.GameStateManager.CurrentState;
        if (state is GameStateEnum.Playing)
        {
            graphics.GraphicsDevice.Clear(Color.WhiteSmoke);
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