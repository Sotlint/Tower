using System;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Enums;

namespace Tower.Core.Helpers;

public static class SpriteScaleHelper
{
    public static float GetSpriteScale(this IEnemy enemy)
    {
        var type = enemy.Type;
        return type switch
        {
            EnemyTypeEnum.Fast => 0.05f,
            EnemyTypeEnum.Basic => 0.05f,
            EnemyTypeEnum.Armored => 0.05f,
            EnemyTypeEnum.Boss => 0.05f,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static float GetSpriteScale(this ITower tower)
    {
        var type = tower.Type;
        return type switch
        {
            TowerTypeEnum.Basic => 0.1f,
            TowerTypeEnum.Fire => 0.1f,
            TowerTypeEnum.Frost => 0.1f,
            TowerTypeEnum.Bomber => 0.1f,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static float GetSpriteScale(this IBuilding building)
        => building.SpriteScale;
}