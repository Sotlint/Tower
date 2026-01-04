using System;
using Microsoft.Xna.Framework;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.Core.Towers;

/// <summary>
/// Огненная башня - башня с огненной атакой.
/// Наследуется от BaseTower и использует спрайт FireTowerSprite.
/// </summary>
public class FireTower : BaseTower
{
    /// <summary>Тип башни - Fire</summary>
    public override TowerTypeEnum Type => TowerTypeEnum.Fire;

    /// <summary>
    /// Создает новую огненную башню
    /// </summary>
    /// <param name="position">Позиция башни</param>
    public FireTower(Vector2 position)
        : base(
            position: position,
            sprite: SpriteManager.FireTowerSprite,
            maxHealth: 180,
            attackPower: 30,
            attackRange: 140f,
            attackDelay: TimeSpan.FromSeconds(0.8), // Быстрее чем каменная
            spriteScale: 1.0f)
    {
    }
}
