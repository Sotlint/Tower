using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.Factories;

public static class EnemyFactory
{
    public static List<IEnemy> CreateEnemy(EnemyTypeEnum type, int count)
        => type switch
        {
            EnemyTypeEnum.Basic => CreateBasicEnemy(count, SpriteManager.BaseEnemySprite),
            EnemyTypeEnum.Fast => throw new NotImplementedException(),
            EnemyTypeEnum.Armored => throw new NotImplementedException(),
            EnemyTypeEnum.Boss => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };


    private static List<IEnemy> CreateBasicEnemy(int count, Texture2D sprite)
    {
        var enemies = new List<IEnemy>();
        for (var i = 0; i < count; i++)
        {
            enemies.Add(new BasicEnemy(sprite, Guid.NewGuid(), new Vector2(10, 10), 50, 5, 10,
                50, EnemyTypeEnum.Basic, TimeSpan.FromMilliseconds(1000),
                new HealthBar(SpriteManager.HealthBarSprite)));
        }

        return enemies;
    }
}