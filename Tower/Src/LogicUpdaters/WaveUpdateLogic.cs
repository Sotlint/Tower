using System;
using Microsoft.Xna.Framework;
using Tower.Src.Managers;
using Tower.Src.Models.Abstractions;

namespace Tower.Src.LogicUpdaters;

public static class WaveUpdateLogic
{
    public static void Update(GameTime gameTime, GameManager gameManager)
    {
        var towers = gameManager.TowerManager.GetTowers();
        var enemies = gameManager.EnemyManager.GetEnemies();
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
                break;
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