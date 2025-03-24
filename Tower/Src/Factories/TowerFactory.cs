using System;
using Microsoft.Xna.Framework;
using Tower.Core;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.Factories;

public static class TowerFactory
{
    public static ITower CreateTower(TowerTypeEnum type, Vector2 position, SpriteManager spriteManager)
        => type switch
        {
            TowerTypeEnum.Basic => CreateBasicTower(position, spriteManager),
            TowerTypeEnum.Fire => throw new NotImplementedException(),
            TowerTypeEnum.Frost => throw new NotImplementedException(),
            TowerTypeEnum.Bomber => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };


    private static ITower CreateBasicTower(Vector2 position, SpriteManager spriteManager) =>
        new BasicTower(Guid.NewGuid(), spriteManager.TowerSprite, position, TowerTypeEnum.Basic, 100, 10, 200,
            TimeSpan.FromMilliseconds(100));
}