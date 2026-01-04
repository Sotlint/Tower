using System;
using Microsoft.Xna.Framework;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.Core.Towers;

/// <summary>
/// Башня света - башня со световой атакой.
/// Наследуется от BaseTower и использует спрайт LightTowerSprite.
/// </summary>
public class LightTower : BaseTower
{
    /// <summary>Тип башни - Light</summary>
    public override TowerTypeEnum Type => TowerTypeEnum.Light;

    /// <summary>
    /// Создает новую башню света
    /// </summary>
    /// <param name="position">Позиция башни</param>
    public LightTower(Vector2 position)
        : base(
            position: position,
            sprite: SpriteManager.LightTowerSprite,
            maxHealth: 170,
            attackPower: 35,
            attackRange: 130f,
            attackDelay: TimeSpan.FromSeconds(0.9),
            spriteScale: 1.0f)
    {
    }
}
