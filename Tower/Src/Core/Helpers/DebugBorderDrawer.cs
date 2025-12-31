using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Managers;

namespace Tower.Core.Helpers;

/// <summary>
/// Вспомогательный класс для отрисовки отладочной информации.
/// Используется в режиме отладки (Ctrl+D) для визуализации границ объектов
/// и радиусов атаки.
/// </summary>
public class DebugBorderDrawer
{
    /// <summary>
    /// Отрисовка всей отладочной информации для объекта:
    /// границы коллизий и радиус атаки.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="bounds">Границы объекта (прямоугольник коллизий)</param>
    /// <param name="attackRange">Радиус атаки объекта</param>
    /// <param name="position">Позиция объекта (центр для радиуса атаки)</param>
    public static void DrawDebug(SpriteBatch spriteBatch, Rectangle bounds, float attackRange, Vector2 position)
    {
        // Отрисовываем границы коллизий (прямоугольник)
        DrawCollisions(spriteBatch, bounds);
        // Отрисовываем радиус атаки (круг)
        DrawAttackRange(spriteBatch, attackRange, position);
    }

    /// <summary>
    /// Отрисовка границ коллизий объекта (прямоугольник).
    /// Отрисовывает зеленые линии по периметру прямоугольника границ.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="bounds">Границы объекта (прямоугольник)</param>
    public static void DrawCollisions(SpriteBatch spriteBatch, Rectangle bounds)
    {
        // Верхняя граница (горизонтальная линия сверху)
        spriteBatch.Draw(SpriteManager.GreenDebugPen, new Rectangle(bounds.X, bounds.Y, bounds.Width, 1), Color.White);

        // Нижняя граница (горизонтальная линия снизу)
        spriteBatch.Draw(SpriteManager.GreenDebugPen,
            new Rectangle(bounds.X, bounds.Y + bounds.Height - 1, bounds.Width, 1), Color.White);

        // Левая граница (вертикальная линия слева)
        spriteBatch.Draw(SpriteManager.GreenDebugPen, new Rectangle(bounds.X, bounds.Y, 1, bounds.Height), Color.White);

        // Правая граница (вертикальная линия справа)
        spriteBatch.Draw(SpriteManager.GreenDebugPen,
            new Rectangle(bounds.X + bounds.Width - 1, bounds.Y, 1, bounds.Height), Color.White);
    }

    /// <summary>
    /// Отрисовка радиуса атаки объекта (круг).
    /// Отрисовывает круг вокруг объекта, показывающий радиус атаки.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="attackRange">Радиус атаки</param>
    /// <param name="position">Позиция объекта (центр круга)</param>
    /// <param name="color">Цвет круга (по умолчанию белый)</param>
    public static void DrawAttackRange(SpriteBatch spriteBatch, float attackRange, Vector2 position, Color? color = null)
    {
        // Используем переданный цвет или белый по умолчанию
        var circleColor = color ?? Color.White;
        
        // Количество сегментов для аппроксимации круга (больше = более гладкий круг)
        var segments = 128;
        // Угол между сегментами
        float angleIncrement = MathF.PI * 2 / segments;

        // Начальная точка (справа от центра)
        Vector2 prevPoint = position + new Vector2(attackRange, 0);

        // Отрисовываем круг как последовательность линий
        for (int i = 1; i <= segments; i++)
        {
            // Вычисляем угол для текущего сегмента
            float angle = angleIncrement * i;
            // Вычисляем позицию точки на окружности
            Vector2 nextPoint = position + new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * attackRange;

            // Отрисовываем линию от предыдущей точки к следующей
            DrawLine(spriteBatch, SpriteManager.GreenDebugPen, prevPoint, nextPoint, circleColor);
            prevPoint = nextPoint;
        }
    }

    /// <summary>
    /// Отрисовка линии между двумя точками. Используется для отрисовки
    /// границ и радиуса атаки в режиме отладки.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="texture">Текстура для отрисовки линии (однопиксельная)</param>
    /// <param name="start">Начальная точка линии</param>
    /// <param name="end">Конечная точка линии</param>
    /// <param name="color">Цвет линии</param>
    /// <param name="thickness">Толщина линии (по умолчанию 1 пиксель)</param>
    private static void DrawLine(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 end, Color color,
        float thickness = 1f)
    {
        // Вычисляем вектор направления линии
        Vector2 edge = end - start;
        // Вычисляем угол поворота линии
        float angle = MathF.Atan2(edge.Y, edge.X);
        // Вычисляем длину линии
        float length = edge.Length();

        // Отрисовываем линию как повернутый прямоугольник
        spriteBatch.Draw(
            texture, // Текстура (однопиксельная)
            start, // Начальная позиция
            null, // Область текстуры
            color, // Цвет
            angle, // Угол поворота
            Vector2.Zero, // Точка привязки
            new Vector2(length, thickness), // Масштаб (длина и толщина)
            SpriteEffects.None, // Эффекты
            0f // Глубина слоя
        );
    }
}