using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Base;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.Factories;

public static class ProjectileFactory
{
    
    public static IProjectile CreateProjectile(ProjectileTypeEnum type, Vector2 position, IEnemy target)
        => type switch
        {
            ProjectileTypeEnum.Orb => CreateOrb(position, SpriteManager.OrbProjectileSprite, target),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

    private static IProjectile CreateOrb(Vector2 position, Texture2D sprite, IEnemy target)
        => new OrbProjectile(position, 10, 10, TimeSpan.FromSeconds(1), 10, 1,
            sprite, target);
}