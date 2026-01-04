using System;
using Microsoft.Xna.Framework;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.Core.Towers;

/// <summary>
/// Ядовитая башня - башня с ядовитой атакой.
/// Наследуется от BaseTower и использует спрайт PoisonTowerSprite.
/// </summary>
public class PoisonTower : BaseTower
{
    /// <summary>Тип башни - Poison</summary>
    public override TowerTypeEnum Type => TowerTypeEnum.Poison;

    /// <summary>
    /// Создает новую ядовитую башню
    /// </summary>
    /// <param name="position">Позиция башни</param>
    public PoisonTower(Vector2 position)
        : base(
            position: position,
            sprite: SpriteManager.PoisonTowerSprite,
            maxHealth: 160,
            attackPower: 28,
            attackRange: 145f,
            attackDelay: TimeSpan.FromSeconds(1.1),
            spriteScale: 1.0f)
    {
    }
}
