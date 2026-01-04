using System;
using Microsoft.Xna.Framework;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.Core.Towers;

/// <summary>
/// Каменная башня - базовая башня с обычной атакой.
/// Наследуется от BaseTower и использует спрайт StoneTowerSprite.
/// </summary>
public class StoneTower : BaseTower
{
    /// <summary>Тип башни - Stone</summary>
    public override TowerTypeEnum Type => TowerTypeEnum.Stone;

    /// <summary>
    /// Создает новую каменную башню
    /// </summary>
    /// <param name="position">Позиция башни</param>
    public StoneTower(Vector2 position)
        : base(
            position: position,
            sprite: SpriteManager.StoneTowerSprite,
            maxHealth: 200,
            attackPower: 25,
            attackRange: 150f, // 150 пикселей радиус атаки
            attackDelay: TimeSpan.FromSeconds(1.0), // Атака раз в секунду
            spriteScale: 1.0f)
    {
    }
}
