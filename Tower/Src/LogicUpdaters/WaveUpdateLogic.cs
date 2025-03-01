using System;
using Microsoft.Xna.Framework;
using Tower.Managers;
using Tower.Models.Abstractions;

namespace Tower.LogicUpdaters;

public static class WaveUpdateLogic
{
    public static void Update(GameTime gameTime, GameManager gameManager)
    {
        var towers = gameManager.TowerManager.GetTowers();
        var enemies = gameManager.EnemyManager.GetAliveEnemies();
        var citadel = gameManager.CitadelManager.GetCitadel();
        citadel.Attack(enemies);
        foreach (var tower in towers)
        {
            
        }
        
        var aliveEnemies = gameManager.EnemyManager.GetAliveEnemies();
        foreach (var aliveEnemy in aliveEnemies)
        {
            aliveEnemy.Update(citadel);
            if (citadel.IsDestroyed())
            {
                gameManager.GameStateManager.ChangeState(GameStateEnum.GameOver);
                return;
            }
        }

        if (aliveEnemies.Count == 0)
        {
            Console.WriteLine("Волна завершена!");
            Console.WriteLine("Сложность увеличена!");
            gameManager.DifficultyManager.IncreaseDifficulty();
            gameManager.GameStateManager.ChangeState(GameStateEnum.Planning);
        }
    }
}