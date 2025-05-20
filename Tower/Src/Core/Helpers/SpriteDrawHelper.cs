using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower.Core.Helpers;

public static class SpriteDrawHelper
{
    public static void DrawSprite(this SpriteBatch spriteBatch, Texture2D sprite, Vector2 position, float scale = 1f,
        float layerDepth = 1f)
    {
        spriteBatch.Draw(
            sprite, // текстура
            position, // позиция
            null, // исходный прямоугольник (null = вся текстура)
            Color.White, // цвет (без изменений)
            0f, // поворот
            new Vector2((float)sprite.Width / 2, (float)sprite.Height / 2), // точка привязки (верхний левый угол)
            scale, // масштаб
            SpriteEffects.None, // эффекты (например, зеркальное отражение)
            1f // слой (глубина)
        );
    }
}