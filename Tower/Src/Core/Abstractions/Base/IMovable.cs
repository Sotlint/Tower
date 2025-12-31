using Microsoft.Xna.Framework;

namespace Tower.Core.Abstractions.Base;

/// <summary>
/// Интерфейс для объектов, которые могут двигаться.
/// Наследуется от IHavePosition и добавляет скорость и метод движения.
/// Реализуется врагами и снарядами.
/// </summary>
public interface IMovable : IHavePosition
{
    /// <summary>Скорость движения объекта</summary>
    int Speed { get; }
    
    /// <summary>
    /// Движение объекта к указанной позиции. Вычисляет направление
    /// и перемещает объект с учетом скорости.
    /// </summary>
    /// <param name="targetPosition">Целевая позиция для движения</param>
    void Move(Vector2 targetPosition);
}