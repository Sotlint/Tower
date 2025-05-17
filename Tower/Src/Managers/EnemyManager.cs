using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Enums;
using Tower.Factories;

namespace Tower.Managers;

/// <summary>
/// Менеджер врагов, отвечающий за управление коллекцией врагов в игре.
/// Позволяет добавлять, удалять, обновлять и получать врагов, а также создавать новых врагов
///  с помощью фабрики <seealso cref="EnemyFactory"/>.
/// </summary>
public class EnemyManager
{
    private List<IEnemy> Enemies { get; set; } = new();

    private void AddEnemy(List<IEnemy> enemy)
        => Enemies.AddRange(enemy);

    private void RemoveEnemy(IEnemy enemy)
        => Enemies.Remove(enemy);
    
    public void UpdateEnemies(GameTime gameTime)
    {
        for (var i = Enemies.Count - 1; i >= 0; i--)
        {
            if (Enemies[i].IsDie())
            {
                RemoveEnemy(Enemies[i]);
            }
            else
            {
                Enemies[i].Update(gameTime);
            }
        }
    }

    public void DrawEnemies(SpriteBatch spriteBatch, GameTime gameTime)
    {
        foreach (var projectile in Enemies)
        {
            projectile.Draw(spriteBatch, gameTime);
        }
    }

    public List<IEnemy> GetEnemies() => Enemies;

    public void SpawnEnemy(int count)
        => AddEnemy(EnemyFactory.CreateEnemy(EnemyTypeEnum.Basic, count));
}