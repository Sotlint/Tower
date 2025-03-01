using System;
using System.Numerics;
using Tower.Managers;
using Tower.Models;
using Tower.Models.Abstractions;

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
        new BasicTower(100, 5, 10, position, TowerTypeEnum.Basic, spriteManager.TowerSprite);
}