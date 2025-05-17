using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions.Base;

namespace Tower.Managers;

public class ProjectileManager
{
    private List<IProjectile> Projectiles { get; set; } = new();

    public void AddProjectile(IProjectile enemy)
        => Projectiles.Add(enemy);

    private void RemoveProjectile(IProjectile projectile)
    {
        Projectiles.Remove(projectile);
    }

    public void UpdateProjectiles(GameTime gameTime)
    {
        for (var i = Projectiles.Count - 1; i >= 0; i--)
        {
            if (Projectiles[i].Target.IsDie() || Projectiles[i].IsAttacked)
            {
                RemoveProjectile(Projectiles[i]);
            }
            else
            {
                Projectiles[i].Update(gameTime);
            }
        }
    }

    public void DrawProjectiles(SpriteBatch spriteBatch, GameTime gameTime)
    {
        foreach (var projectile in Projectiles)
        {
            projectile.Draw(spriteBatch, gameTime);
        }
    }
}