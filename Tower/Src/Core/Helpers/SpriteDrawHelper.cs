using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower.Core.Helpers;

/// <summary>
/// Вспомогательный класс для отрисовки спрайтов. Предоставляет extension-метод
/// для удобной отрисовки спрайтов с центрированием по точке привязки.
/// </summary>
public static class SpriteDrawHelper
{
    /// <summary>
    /// Отрисовка спрайта с центрированием. Extension-метод для SpriteBatch,
    /// который отрисовывает спрайт с центрированием по точке привязки (центр спрайта).
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="sprite">Текстура спрайта для отрисовки</param>
    /// <param name="position">Позиция спрайта на экране (центр спрайта)</param>
    /// <param name="scale">Масштаб спрайта (1.0 = исходный размер)</param>
    /// <param name="layerDepth">Глубина слоя для сортировки (0.0 = дальний план, 1.0 = ближний план)</param>
    public static void DrawSprite(this SpriteBatch spriteBatch, Texture2D sprite, Vector2 position, float scale = 1f,
        float layerDepth = 0.5f)
    {
        spriteBatch.Draw(
            sprite, // Текстура спрайта
            position, // Позиция (центр спрайта)
            null, // Область текстуры (null = вся текстура)
            Color.White, // Цвет (без изменений)
            0f, // Угол поворота
            new Vector2((float)sprite.Width / 2, (float)sprite.Height / 2), // Точка привязки (центр спрайта)
            scale, // Масштаб
            SpriteEffects.None, // Эффекты отображения (без эффектов)
            layerDepth // Глубина слоя
        );
    }
}