using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Base;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.Factories;

/// <summary>
/// Фабрика для создания снарядов. Предоставляет методы для создания снарядов различных типов.
/// Снаряды создаются башнями при атаке врагов.
/// </summary>
public static class ProjectileFactory
{
    /// <summary>
    /// Создать снаряд указанного типа. Снаряд будет лететь к указанной цели (врагу).
    /// </summary>
    /// <param name="type">Тип снаряда для создания</param>
    /// <param name="position">Начальная позиция снаряда (позиция башни, которая стреляет)</param>
    /// <param name="target">Цель снаряда (враг, в которого стреляют)</param>
    /// <returns>Созданный снаряд, реализующий интерфейс IProjectile</returns>
    /// <exception cref="ArgumentOutOfRangeException">Если передан неизвестный тип снаряда</exception>
    public static IProjectile CreateProjectile(ProjectileTypeEnum type, Vector2 position, IEnemy target)
        => type switch
        {
            ProjectileTypeEnum.Orb => CreateOrb(position, SpriteManager.OrbProjectileSprite, target),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

    /// <summary>
    /// Создать снаряд типа "Orb" (сфера). Снаряд летит к цели и наносит урон при достижении.
    /// Характеристики: урон 10, радиус атаки 10, скорость 10, масштаб 1.
    /// </summary>
    /// <param name="position">Начальная позиция снаряда</param>
    /// <param name="sprite">Текстура спрайта снаряда</param>
    /// <param name="target">Цель снаряда (враг)</param>
    /// <returns>Созданный снаряд типа Orb</returns>
    private static IProjectile CreateOrb(Vector2 position, Texture2D sprite, IEnemy target)
        => new OrbProjectile(
            position, // Начальная позиция (позиция башни)
            10, // Сила атаки (урон)
            10, // Радиус атаки (расстояние для попадания)
            TimeSpan.FromSeconds(1), // Задержка атаки (не используется для снарядов)
            10, // Скорость движения
            1, // Масштаб спрайта
            sprite, // Текстура спрайта
            target // Цель (враг)
        );
}