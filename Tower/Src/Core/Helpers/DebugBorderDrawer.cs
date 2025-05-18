using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Managers;

namespace Tower.Core.Helpers;

public class DebugBorderDrawer
{
    public static void DrawDebug(SpriteBatch spriteBatch, Rectangle bounds)
    {
        DrawCollisions(spriteBatch, bounds);
    }

    public static void DrawCollisions(SpriteBatch spriteBatch, Rectangle bounds)
    {
        // Верхняя граница
        spriteBatch.Draw(SpriteManager.GreenDebugPen, new Rectangle(bounds.X, bounds.Y, bounds.Width, 1), Color.White);

        // Нижняя граница
        spriteBatch.Draw(SpriteManager.GreenDebugPen,
            new Rectangle(bounds.X, bounds.Y + bounds.Height - 1, bounds.Width, 1), Color.White);

        // Левая граница
        spriteBatch.Draw(SpriteManager.GreenDebugPen, new Rectangle(bounds.X, bounds.Y, 1, bounds.Height), Color.White);

        // Правая граница
        spriteBatch.Draw(SpriteManager.GreenDebugPen,
            new Rectangle(bounds.X + bounds.Width - 1, bounds.Y, 1, bounds.Height), Color.White);
    }
}