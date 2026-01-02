using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;

namespace Tower.Core.Common;

/// <summary>
/// Полоса здоровья. Отрисовывает визуальное представление здоровья объекта
/// (врага или здания) в виде полосы над объектом.
/// </summary>
public class HealthBar
{
    /// <summary>Текстура для отрисовки полосы здоровья (однопиксельная текстура для заливки)</summary>
    private Texture2D Sprite { get; set; }

    /// <summary>
    /// Приватный конструктор (не используется, требуется для инициализации)
    /// </summary>
    private HealthBar()
    {
    }

    /// <summary>
    /// Конструктор полосы здоровья. Принимает текстуру для отрисовки.
    /// </summary>
    /// <param name="sprite">Текстура для отрисовки (обычно однопиксельная)</param>
    public HealthBar(Texture2D sprite)
    {
        Sprite = sprite;
    }

    /// <summary>
    /// Отрисовка полосы здоровья для врага. Отрисовывает полосу над врагом,
    /// показывающую текущее здоровье в процентах.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="gameTime">Время игры (не используется, но требуется для совместимости)</param>
    /// <param name="enemy">Враг, для которого отрисовывается полоса здоровья</param>
    public void Draw(SpriteBatch spriteBatch, GameTime gameTime, IEnemy enemy)
    {
        // Не отрисовываем полосу, если враг уже мертв
        if (enemy.Health <= 0)
            return;

        // Вычисляем процент здоровья (от 0 до 1)
        var healthPercent = MathHelper.Clamp((float)enemy.Health / enemy.MaxHealth, 0, 1);

        // Цвета полосы: темно-красный фон, зеленый заполнитель
        var backColor = Color.DarkRed; // Фон полосы (показывает максимальное здоровье)
        var frontColor = Color.LimeGreen; // Заполнитель (показывает текущее здоровье)

        // Получаем границы спрайта врага
        var bounds = enemy.Bounds;
        
        // Параметры полосы здоровья
        var barWidth = (int)(bounds.Width * 0.8f); // Ширина полосы (80% от ширины спрайта)
        var barHeight = 4; // Высота полосы (уменьшена для более аккуратного вида)
        var barOffset = 3; // Отступ от верхней границы спрайта (в пикселях)
        
        // Позиция полосы: над верхней границей спрайта, центрирована по горизонтали
        var barX = bounds.X + bounds.Width / 2 - barWidth / 2; // Центрирование по X
        var barY = bounds.Y - barHeight - barOffset; // Над верхней границей спрайта

        // Отрисовка фона полосы (темно-красный прямоугольник)
        spriteBatch.Draw(
            texture: Sprite,
            destinationRectangle: new Rectangle((int)barX, (int)barY, barWidth, barHeight),
            color: backColor
        );

        // Отрисовка заполнителя полосы (зеленый прямоугольник, ширина зависит от здоровья)
        spriteBatch.Draw(
            texture: Sprite,
            destinationRectangle: new Rectangle((int)barX, (int)barY, (int)(barWidth * healthPercent), barHeight),
            color: frontColor
        );
    }

    /// <summary>
    /// Отрисовка полосы здоровья для здания (цитадель или башня).
    /// Отрисовывает полосу над зданием, показывающую текущее здоровье в процентах.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="gameTime">Время игры (не используется, но требуется для совместимости)</param>
    /// <param name="building">Здание, для которого отрисовывается полоса здоровья</param>
    public void Draw(SpriteBatch spriteBatch, GameTime gameTime, IBuilding building)
    {
        // Не отрисовываем полосу, если здание уже уничтожено
        if (building.Health <= 0)
            return;

        // Вычисляем процент здоровья (от 0 до 1)
        var healthPercent = MathHelper.Clamp((float)building.Health / building.MaxHealth, 0, 1);

        // Цвета полосы: темно-красный фон, зеленый заполнитель
        var backColor = Color.DarkRed; // Фон полосы (показывает максимальное здоровье)
        var frontColor = Color.LimeGreen; // Заполнитель (показывает текущее здоровье)

        // Получаем границы спрайта здания
        var bounds = building.Bounds;
        
        // Параметры полосы здоровья
        var barWidth = (int)(bounds.Width * 0.8f); // Ширина полосы (80% от ширины спрайта)
        var barHeight = 4; // Высота полосы (уменьшена для более аккуратного вида)
        var barOffset = 3; // Отступ от верхней границы спрайта (в пикселях)
        
        // Позиция полосы: над верхней границей спрайта, центрирована по горизонтали
        var barX = bounds.X + bounds.Width / 2 - barWidth / 2; // Центрирование по X
        var barY = bounds.Y - barHeight - barOffset; // Над верхней границей спрайта

        // Отрисовка фона полосы (темно-красный прямоугольник)
        spriteBatch.Draw(
            texture: Sprite,
            destinationRectangle: new Rectangle((int)barX, (int)barY, barWidth, barHeight),
            color: backColor
        );

        // Отрисовка заполнителя полосы (зеленый прямоугольник, ширина зависит от здоровья)
        spriteBatch.Draw(
            texture: Sprite,
            destinationRectangle: new Rectangle((int)barX, (int)barY, (int)(barWidth * healthPercent), barHeight),
            color: frontColor
        );
    }
}