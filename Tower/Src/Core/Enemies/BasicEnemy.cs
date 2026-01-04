using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.Core.Enemies;

/// <summary>
/// Базовый враг - стандартный тип врага с обычными характеристиками.
/// Наследуется от BaseEnemy и использует спрайт BaseEnemySprite.
/// </summary>
public class BasicEnemy : BaseEnemy
{
    /// <summary>Тип врага - Basic</summary>
    public override EnemyTypeEnum Type => EnemyTypeEnum.Basic;
    
    /// <summary>Награда за убийство базового врага</summary>
    public override int Reward => 10;

    /// <summary>
    /// Создает нового базового врага с стандартными характеристиками
    /// </summary>
    /// <param name="position">Начальная позиция врага</param>
    public BasicEnemy(Vector2 position)
        : base(
            position: position,
            sprite: SpriteManager.BaseEnemySprite,
            speed: 50, // 50 пикселей в секунду
            maxHealth: 100,
            attackPower: 10,
            attackRange: 30f, // 30 пикселей радиус атаки
            attackDelay: TimeSpan.FromSeconds(1.0), // Атака раз в секунду
            spriteScale: 1.0f)
    {
    }
}
