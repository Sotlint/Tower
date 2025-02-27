using System;
using System.Numerics;
using Tower.Src.Models;
using Tower.Src.Models.Abstractions;

namespace Tower.Src.Factories;

public static class TowerFactory
{
    public static ITower CreateTower(TowerTypeEnum type, Vector2 position)
        => type switch
        {
            TowerTypeEnum.Basic => CreateBasicTower(position),
            TowerTypeEnum.Fire => throw new NotImplementedException(),
            TowerTypeEnum.Frost => throw new NotImplementedException(),
            TowerTypeEnum.Bomber => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };


    private static ITower CreateBasicTower(Vector2 position) =>
        new BasicTower(100, 5, 10, position, TowerTypeEnum.Basic);
}