using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower.Core.Abstractions.Base;

/// <summary>
/// Интерфейс для объектов, которые имеют логику отрисовки.
/// Наследуется от IHaveSprite и добавляет метод Draw.
/// Реализуется врагами, башнями, цитаделью и снарядами.
/// </summary>
public interface IHaveDrawLogic : IHaveSprite
{
    /// <summary>
    /// Отрисовка объекта. Вызывается каждый кадр для отрисовки объекта на экране.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="gameTime">Время игры для синхронизации анимаций</param>
    void Draw(SpriteBatch spriteBatch, GameTime gameTime);
}