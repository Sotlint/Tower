using System;
using Microsoft.Xna.Framework;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.LogicUpdaters;

public static class WaveUpdateLogic
{
    public static void Update(GameTime gameTime, GameManager gameManager)
    {
        var towers = gameManager.TowerManager.GetTowers();
        
        // действия цитадели
        var citadel = gameManager.CitadelManager.GetCitadel();
        citadel.Update(gameManager, gameTime);
        
        //действия башен
        foreach (var tower in towers)
        {
            tower.Update(gameManager, gameTime);
        }

        //действия врагов
        var enemies = gameManager.EnemyManager.GetEnemies();
        foreach (var enemy in enemies)
        {
            enemy.Update(gameManager, gameTime);
        }

        //проврека окончания волны
        if (gameManager.EnemyManager.GetAliveEnemies().Count == 0)
        {
            Console.WriteLine("Волна завершена!");
            Console.WriteLine("Сложность увеличена!");
            gameManager.DifficultyManager.IncreaseDifficulty();
            gameManager.GameStateManager.ChangeState(GameStateEnum.Planning);
        }
    }
}