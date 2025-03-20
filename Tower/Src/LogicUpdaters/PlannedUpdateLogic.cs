using System;
using Microsoft.Xna.Framework;
using Tower.Managers;
using Tower.Models.Abstractions;
using Tower.Models.Abstractions.Enums;

namespace Tower.LogicUpdaters;

public static class PlannedUpdateLogic
{
    public static void Update(GameTime gameTime, GameManager gameManager)
    {
        gameManager.EnemyManager.SpawnEnemy(
            gameManager.DifficultyManager.GetEnemyCount(),
            gameManager.SpriteManager);
        Console.WriteLine($"Создано новые враги {gameManager.DifficultyManager.GetEnemyCount()}");
        gameManager.GameStateManager.ChangeState(GameStateEnum.Playing);
    }
}