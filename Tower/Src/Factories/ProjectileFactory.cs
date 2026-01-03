using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core;
using Tower.Core.Abstractions;
using GameKit2D.Abstractions.Interfaces.GameObjects;
using Tower.Core.Abstractions.Enums;
using Tower.Core.Projectiles;
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
    /// <param name="attackPower">Сила атаки снаряда (урон, наносимый врагу)</param>
    /// <returns>Созданный снаряд, реализующий интерфейс IProjectile из GameKit2D</returns>
    /// <exception cref="ArgumentOutOfRangeException">Если передан неизвестный тип снаряда</exception>
    public static GameKit2D.Abstractions.Interfaces.GameObjects.IProjectile CreateProjectile(ProjectileTypeEnum type, Vector2 position, IEnemy target, decimal attackPower)
        => type switch
        {
            ProjectileTypeEnum.Orb => CreateOrb(position, SpriteManager.OrbProjectileSprite, target, attackPower),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

    /// <summary>
    /// Создать снаряд типа "Orb" (сфера). Снаряд летит к цели и наносит урон при достижении.
    /// Характеристики: радиус атаки 10, скорость 10, масштаб 1.
    /// </summary>
    /// <param name="position">Начальная позиция снаряда</param>
    /// <param name="sprite">Текстура спрайта снаряда</param>
    /// <param name="target">Цель снаряда (враг)</param>
    /// <param name="attackPower">Сила атаки снаряда (урон, наносимый врагу)</param>
    /// <returns>Созданный снаряд типа Orb</returns>
    private static GameKit2D.Abstractions.Interfaces.GameObjects.IProjectile CreateOrb(Vector2 position, Texture2D sprite, IEnemy target, decimal attackPower)
        => new OrbProjectile(
            position, // Начальная позиция (позиция башни)
            attackPower, // Сила атаки (урон) - передается от башни
            10, // Радиус атаки (расстояние для попадания)
            TimeSpan.FromSeconds(1), // Задержка атаки (не используется для снарядов)
            10, // Скорость движения
            1, // Масштаб спрайта
            sprite, // Текстура спрайта
            target // Цель (враг)
        );
}