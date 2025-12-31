using System;
using Microsoft.Xna.Framework;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.LogicUpdaters;

public static class WaveUpdateLogic
{
    public static void Update(GameTime gameTime)
    {
        // действия
        GameManager.CitadelManager.UpdateCitadel(gameTime);
        GameManager.TowerManager.UpdateTowers(gameTime);
        GameManager.ProjectileManager.UpdateProjectiles(gameTime);
        GameManager.EnemyManager.UpdateEnemies(gameTime);

        //проверка окончания волны
        if (GameManager.EnemyManager.GetEnemies().Count == 0)
        {
            GameManager.DifficultyManager.IncreaseDifficulty();
            PlannedUpdateLogic.Reset(); // Сбрасываем состояние планирования
            GameManager.GameStateManager.ChangeState(GameStateEnum.Planning);
        }
    }
}