using GameKit2D.Core.GameObjects;
using Microsoft.Xna.Framework.Graphics;

namespace Tower.Core.Common;

/// <summary>
/// Полоса здоровья. Отрисовывает визуальное представление здоровья объекта
/// (врага или здания) в виде полосы над объектом.
/// </summary>
public class HealthBar : DrawableGameObject
{
    /// <summary>
    /// Конструктор полосы здоровья. Принимает текстуру для отрисовки.
    /// </summary>
    /// <param name="sprite">Текстура для отрисовки (обычно однопиксельная)</param>
    public HealthBar(Texture2D sprite) : base(sprite)
    {
        Sprite = sprite;
    }
}