using Microsoft.Xna.Framework;
using Tower.Managers;

namespace Tower.Core.Abstractions.Base;

/// <summary>
/// Интерфейс для объектов, которые имеют игровую логику обновления.
/// Реализуется врагами, башнями, цитаделью и снарядами.
/// </summary>
public interface IHaveGameLogic
{
    /// <summary>
    /// Обновление объекта. Вызывается каждый кадр для обновления состояния объекта.
    /// </summary>
    /// <param name="gameTime">Время игры для синхронизации обновлений</param>
    void Update(GameTime gameTime);
}