using System;
using Microsoft.Xna.Framework;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.LogicUpdaters;

public static class WaveUpdateLogic
{
    public static void Update(GameTime gameTime)
    {
        var towers = GameManager.TowerManager.GetTowers();

        // действия цитадели
        var citadel = GameManager.CitadelManager.GetCitadel();
        citadel.Update(gameTime);

        //действия башен
        foreach (var tower in towers)
        {
            tower.Update(gameTime);
        }
        
        GameManager.ProjectileManager.UpdateProjectiles(gameTime);

        //действия врагов
        var enemies = GameManager.EnemyManager.GetEnemies();
        foreach (var enemy in enemies)
        {
            enemy.Update(gameTime);
        }

        //проврека окончания волны
        if (GameManager.EnemyManager.GetAliveEnemies().Count == 0)
        {
            Console.WriteLine("Волна завершена!");
            Console.WriteLine("Сложность увеличена!");
            GameManager.DifficultyManager.IncreaseDifficulty();
            GameManager.GameStateManager.ChangeState(GameStateEnum.Planning);
        }
    }
}