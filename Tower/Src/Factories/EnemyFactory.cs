using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Enums;
using Tower.Core.Common;
using Tower.Core.Enemies;
using Tower.Managers;

namespace Tower.Factories;

/// <summary>
/// Фабрика для создания врагов. Предоставляет методы для создания врагов различных типов.
/// Все враги спавнятся в одной точке (10, 10) - TODO: добавить систему точек спавна.
/// </summary>
public static class EnemyFactory
{
    /// <summary>
    /// Создать врагов указанного типа в указанном количестве.
    /// </summary>
    /// <param name="type">Тип врага для создания</param>
    /// <param name="count">Количество врагов для создания</param>
    /// <returns>Список созданных врагов</returns>
    /// <exception cref="NotImplementedException">Если тип врага еще не реализован</exception>
    /// <exception cref="ArgumentOutOfRangeException">Если передан неизвестный тип врага</exception>
    public static List<IEnemy> CreateEnemy(EnemyTypeEnum type, int count)
        => type switch
        {
            EnemyTypeEnum.Basic => CreateBasicEnemy(count, SpriteManager.BaseEnemySprite),
            EnemyTypeEnum.Fast => throw new NotImplementedException(),
            EnemyTypeEnum.Armored => throw new NotImplementedException(),
            EnemyTypeEnum.Boss => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

    /// <summary>
    /// Создать базовых врагов. Базовый враг имеет стандартные характеристики:
    /// здоровье 50, скорость 2, урон 10, радиус атаки 50, задержка атаки 1000мс.
    /// </summary>
    /// <param name="count">Количество врагов для создания</param>
    /// <param name="sprite">Текстура спрайта врага</param>
    /// <returns>Список созданных базовых врагов</returns>
    private static List<IEnemy> CreateBasicEnemy(int count, Texture2D sprite)
    {
        var enemies = new List<IEnemy>();
        
        // Создаем указанное количество врагов
        for (var i = 0; i < count; i++)
        {
            // TODO: Добавить систему точек спавна вместо фиксированной позиции (10, 10)
            enemies.Add(new BasicEnemy(
                sprite, // Текстура спрайта
                Guid.NewGuid(), // Уникальный идентификатор
                new Vector2(10, 10), // Позиция спавна (TODO: использовать точки спавна)
                50, // Здоровье
                2, // Скорость движения
                10, // Сила атаки
                50, // Радиус атаки
                EnemyTypeEnum.Basic, // Тип врага
                TimeSpan.FromMilliseconds(1000), // Задержка между атаками (1000мс)
                new HealthBar(SpriteManager.HealthBarSprite) // Полоса здоровья
            ));
        }

        return enemies;
    }
}