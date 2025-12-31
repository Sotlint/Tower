using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Base;

namespace Tower.Core;

/// <summary>
/// Снаряд типа "Orb" (сфера). Реализует интерфейс IProjectile.
/// Снаряд летит к цели (врагу) и наносит урон при достижении.
/// </summary>
public partial class OrbProjectile : IProjectile
{
    /// <summary>
    /// Конструктор снаряда Orb. Инициализирует все свойства снаряда.
    /// </summary>
    /// <param name="position">Начальная позиция снаряда (позиция башни, которая стреляет)</param>
    /// <param name="attackPower">Сила атаки (урон, наносимый врагу)</param>
    /// <param name="attackRange">Радиус атаки (расстояние для попадания в цель)</param>
    /// <param name="attackDelay">Задержка между атаками (не используется для снарядов)</param>
    /// <param name="speed">Скорость движения снаряда</param>
    /// <param name="spriteScale">Масштаб спрайта снаряда</param>
    /// <param name="sprite">Текстура спрайта снаряда</param>
    /// <param name="target">Цель снаряда (враг, в которого стреляют)</param>
    public OrbProjectile(Vector2 position, int attackPower, float attackRange, TimeSpan attackDelay, int speed,
        float spriteScale, Texture2D sprite, IEnemy target)
    {
        Position = position;
        AttackPower = attackPower;
        AttackRange = attackRange;
        AttackDelay = attackDelay;
        TimeSinceLastAttack = TimeSpan.Zero;
        Speed = speed;
        SpriteScale = spriteScale;
        Sprite = sprite;
        Target = target;
        IsAttacked = false; // Снаряд еще не попал в цель
    }

    /// <summary>Цель снаряда (враг, в которого стреляют)</summary>
    public IEnemy Target { get; init; }
    
    /// <summary>Флаг, указывающий, попал ли снаряд в цель</summary>
    public bool IsAttacked { get; private set; }
    
    /// <summary>Позиция снаряда на карте</summary>
    public Vector2 Position { get; private set; }
    
    /// <summary>Сила атаки снаряда (урон, наносимый врагу)</summary>
    public int AttackPower { get; init; }
    
    /// <summary>Радиус атаки снаряда (расстояние для попадания в цель)</summary>
    public float AttackRange { get; init; }
    
    /// <summary>Задержка между атаками (не используется для снарядов)</summary>
    public TimeSpan AttackDelay { get; init; }
    
    /// <summary>Время, прошедшее с последней атаки (не используется для снарядов)</summary>
    public TimeSpan TimeSinceLastAttack { get; set; }
    
    /// <summary>Скорость движения снаряда</summary>
    public int Speed { get; init; }
    
    /// <summary>Масштаб спрайта снаряда (для отрисовки)</summary>
    public float SpriteScale { get; init; }
    
    /// <summary>Текстура спрайта снаряда</summary>
    public Texture2D Sprite { get; init; }
}