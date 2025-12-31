namespace Tower.Core.Abstractions.Base;

/// <summary>
/// Интерфейс для объектов, которые могут получать урон и умирать.
/// Реализуется врагами, башнями и цитаделью.
/// </summary>
public interface ICanDie
{
    /// <summary>Максимальное здоровье объекта</summary>
    int MaxHealth { get; }
    
    /// <summary>Текущее здоровье объекта</summary>
    int Health { get; }

    /// <summary>
    /// Получение урона. Уменьшает здоровье на указанное количество.
    /// </summary>
    /// <param name="damage">Количество урона</param>
    void TakeDamage(int damage);

    /// <summary>
    /// Проверка, уничтожен ли объект (здоровье <= 0).
    /// </summary>
    /// <returns>true, если объект уничтожен, false - иначе</returns>
    bool IsDie();
}