using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Tower.Managers;
using Tower.Models;
using Tower.Models.Abstractions;

namespace Tower.Factories;

public static class EnemyFactory
{
    public static List<IEnemy> CreateEnemy(EnemyTypeEnum type, int count, SpriteManager spriteManager)
        => type switch
        {
            EnemyTypeEnum.Basic => CreateBasicEnemy(count, spriteManager),
            EnemyTypeEnum.Fast => throw new NotImplementedException(),
            EnemyTypeEnum.Armored => throw new NotImplementedException(),
            EnemyTypeEnum.Boss => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };


    private static List<IEnemy> CreateBasicEnemy(int count, SpriteManager spriteManager)
    {
        var enemies = new List<IEnemy>();
        for (var i = 0; i < count; i++)
        {
            enemies.Add(new BasicEnemy(100, 5, 10, EnemyTypeEnum.Basic, spriteManager.BaseEnemySprite));
        }

        return enemies;
    }
}