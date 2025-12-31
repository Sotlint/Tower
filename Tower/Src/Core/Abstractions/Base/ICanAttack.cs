using System;
using System.Collections.Generic;

namespace Tower.Core.Abstractions.Base;

/// <summary>
/// Интерфейс для объектов, которые могут атаковать другие объекты.
/// Реализуется башнями, врагами и снарядами.
/// </summary>
public interface ICanAttack
{
    /// <summary>Сила атаки (урон, наносимый целям)</summary>
    int AttackPower { get; }
    
    /// <summary>Радиус атаки (максимальное расстояние до цели)</summary>
    float AttackRange { get; }
    
    /// <summary>Задержка между атаками</summary>
    TimeSpan AttackDelay { get; }
    
    /// <summary>
    /// Атака целей. Наносит урон указанным целям.
    /// </summary>
    /// <param name="targets">Список целей для атаки</param>
    void Attack(IEnumerable<ICanDie> targets);
    
    /// <summary>Время, прошедшее с последней атаки</summary>
    TimeSpan TimeSinceLastAttack { get; }
}