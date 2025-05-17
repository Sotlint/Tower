using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.Factories;

public static class TowerFactory
{
    public static ITower CreateTower(TowerTypeEnum type, Vector2 position)
        => type switch
        {
            TowerTypeEnum.Basic => CreateBasicTower(position, SpriteManager.TowerSprite),
            TowerTypeEnum.Fire => throw new NotImplementedException(),
            TowerTypeEnum.Frost => throw new NotImplementedException(),
            TowerTypeEnum.Bomber => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };


    private static ITower CreateBasicTower(Vector2 position, Texture2D sprite) =>
        new BasicTower(Guid.NewGuid(), sprite, position, TowerTypeEnum.Basic, 100, 10, 100,
            TimeSpan.FromSeconds(1));
}