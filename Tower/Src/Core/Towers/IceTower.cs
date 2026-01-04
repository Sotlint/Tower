using System;
using Microsoft.Xna.Framework;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.Core.Towers;

/// <summary>
/// Ледяная башня - башня с ледяной атакой, замедляющей врагов.
/// Наследуется от BaseTower и использует спрайт IceTowerSprite.
/// </summary>
public class IceTower : BaseTower
{
    /// <summary>Тип башни - Ice</summary>
    public override TowerTypeEnum Type => TowerTypeEnum.Ice;

    /// <summary>
    /// Создает новую ледяную башню
    /// </summary>
    /// <param name="position">Позиция башни</param>
    public IceTower(Vector2 position)
        : base(
            position: position,
            sprite: SpriteManager.IceTowerSprite,
            maxHealth: 150,
            attackPower: 20,
            attackRange: 160f, // Больший радиус
            attackDelay: TimeSpan.FromSeconds(1.2), // Медленнее атака
            spriteScale: 1.0f)
    {
    }
}
