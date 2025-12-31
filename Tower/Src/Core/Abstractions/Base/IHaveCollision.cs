using Microsoft.Xna.Framework;

namespace Tower.Core.Abstractions.Base;

/// <summary>
/// Интерфейс для объектов, которые имеют коллизии (границы для проверки столкновений).
/// Реализуется врагами, башнями и цитаделью.
/// </summary>
public interface IHaveCollision
{
    /// <summary>
    /// Разрешение коллизий. Обрабатывает столкновения с другими объектами.
    /// </summary>
    void ResolveCollision();
    
    /// <summary>Границы объекта для проверки коллизий (прямоугольник)</summary>
    Rectangle Bounds { get; }
    
    /// <summary>
    /// Обновление границ объекта. Вычисляет прямоугольник коллизий на основе
    /// текущей позиции и размера объекта.
    /// </summary>
    void UpdateBounds();
}