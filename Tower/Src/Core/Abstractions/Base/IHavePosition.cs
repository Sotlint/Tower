using Microsoft.Xna.Framework;

namespace Tower.Core.Abstractions.Base;

/// <summary>
/// Интерфейс для объектов, которые имеют позицию на карте.
/// Реализуется всеми объектами, которые имеют координаты в игровом мире.
/// </summary>
public interface IHavePosition
{
    /// <summary>Позиция объекта на карте (координаты X, Y)</summary>
    Vector2 Position { get; }

    /// <summary>
    /// Установка позиции объекта. Может устанавливать абсолютную позицию
    /// или добавлять смещение к текущей позиции (зависит от реализации).
    /// </summary>
    /// <param name="direction">Новая позиция или смещение</param>
    void SetPosition(Vector2 direction);
}