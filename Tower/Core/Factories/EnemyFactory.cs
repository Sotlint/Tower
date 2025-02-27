using System;
using System.Collections.Generic;
using Tower.Core.Models;
using Tower.Core.Models.Abstractions;

namespace Tower.Core.Factories;

public static class EnemyFactory
{
    public static List<IEnemy> CreateEnemy(EnemyTypeEnum type, int count)
        => type switch
        {
            EnemyTypeEnum.Basic => CreateBasicEnemy(count),
            EnemyTypeEnum.Fast => throw new NotImplementedException(),
            EnemyTypeEnum.Armored => throw new NotImplementedException(),
            EnemyTypeEnum.Boss => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };


    private static List<IEnemy> CreateBasicEnemy(int count)
    {
        var enemies = new List<IEnemy>();
        for (var i = 0; i < count; i++)
        {
            enemies.Add(new BasicEnemy(100, 5, 10, EnemyTypeEnum.Basic));
        }

        return enemies;
    }
}