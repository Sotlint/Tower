using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Managers;

namespace Tower.Core.Helpers;

public class DebugBorderDrawer
{
    public static void DrawDebug(SpriteBatch spriteBatch, Rectangle bounds, float attackRange, Vector2 position)
    {
        DrawCollisions(spriteBatch, bounds);
        DrawAttackRange(spriteBatch, attackRange, position);
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

    public static void DrawAttackRange(SpriteBatch spriteBatch, float attackRange, Vector2 position)
    {
        var segments = 128;
        float angleIncrement = MathF.PI * 2 / segments;

        Vector2 prevPoint = position + new Vector2(attackRange, 0);

        for (int i = 1; i <= segments; i++)
        {
            float angle = angleIncrement * i;
            Vector2 nextPoint = position + new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * attackRange;

            DrawLine(spriteBatch, SpriteManager.GreenDebugPen, prevPoint, nextPoint, Color.White);
            prevPoint = nextPoint;
        }
    }

    public static void DrawLine(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 end, Color color,
        float thickness = 1f)
    {
        Vector2 edge = end - start;
        float angle = MathF.Atan2(edge.Y, edge.X);
        float length = edge.Length();

        spriteBatch.Draw(
            texture,
            start,
            null,
            color,
            angle,
            Vector2.Zero,
            new Vector2(length, thickness),
            SpriteEffects.None,
            0f
        );
    }
}