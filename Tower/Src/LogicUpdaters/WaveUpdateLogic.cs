using System;
using Microsoft.Xna.Framework;
using Tower.Managers;
using Tower.Models.Abstractions;
using Tower.Models.Abstractions.Enums;

namespace Tower.LogicUpdaters;

public static class WaveUpdateLogic
{
    public static void Update(GameTime gameTime, GameManager gameManager)
    {
        var towers = gameManager.TowerManager.GetTowers();
        var citadel = gameManager.CitadelManager.GetCitadel();
        citadel.Update(gameManager, gameTime);
        foreach (var tower in towers)
        {
            tower.Update(gameManager, gameTime);
        }

        var enemies = gameManager.EnemyManager.GetEnemies();
        foreach (var enemy in enemies)
        {
            enemy.Update(gameManager, gameTime);
        }

        if (gameManager.EnemyManager.GetAliveEnemies().Count == 0)
        {
            Console.WriteLine("Волна завершена!");
            Console.WriteLine("Сложность увеличена!");
            gameManager.DifficultyManager.IncreaseDifficulty();
            gameManager.GameStateManager.ChangeState(GameStateEnum.Planning);
        }
    }
}