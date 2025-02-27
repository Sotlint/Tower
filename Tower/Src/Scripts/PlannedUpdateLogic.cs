using System;
using Microsoft.Xna.Framework;
using Tower.Src.Managers;
using Tower.Src.Models.Abstractions;

namespace Tower.Src.Scripts;

public static class PlannedUpdateLogic
{
    public static void Update(GameTime gameTime, GameManager gameManager)
    {
        gameManager.EnemyManager.SpawnEnemy(gameManager.DifficultyManager.GetEnemyCount());
        Console.WriteLine($"Создано новые враги {gameManager.DifficultyManager.GetEnemyCount()}");
        gameManager.GameStateManager.ChangeState(GameStateEnum.Playing);
    }
}