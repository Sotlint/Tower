using Microsoft.Xna.Framework;
using GameKit2D.Abstractions.Interfaces.GameObjects;
using Tower.Core.Abstractions;
using Tower.Core.Enemies;
using Tower.Managers;

namespace Tower.Core.Projectiles;

/// <summary>
/// Снаряд типа Orb - базовый снаряд в виде синего круга.
/// Наследуется от BaseProjectile и использует спрайт OrbProjectileSprite.
/// </summary>
public class OrbProjectile : BaseProjectile
{
    /// <summary>
    /// Создает новый снаряд Orb
    /// </summary>
    /// <param name="position">Начальная позиция снаряда</param>
    /// <param name="target">Цель снаряда (враг)</param>
    /// <param name="targetPosition">Позиция цели</param>
    /// <param name="attackPower">Сила атаки (урон)</param>
    public OrbProjectile(
        Vector2 position,
        IEnemy target,
        IHavePosition targetPosition,
        decimal attackPower)
        : base(
            position: position,
            sprite: SpriteManager.OrbProjectileSprite,
            target: target,
            targetPosition: targetPosition,
            speed: 300, // 300 пикселей в секунду
            attackPower: attackPower,
            attackRange: 10f, // 10 пикселей радиус попадания
            spriteScale: 1.0f)
    {
    }
}
