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

        // действия
        GameManager.CitadelManager.UpdateCitadel(gameTime);
        GameManager.TowerManager.UpdateTowers(gameTime);
        GameManager.ProjectileManager.UpdateProjectiles(gameTime);
        GameManager.EnemyManager.UpdateEnemies(gameTime);

        //проврека окончания волны
        if (GameManager.EnemyManager.GetEnemies().Count == 0)
        {
            Console.WriteLine("Волна завершена!");
            Console.WriteLine("Сложность увеличена!");
            GameManager.DifficultyManager.IncreaseDifficulty();
            GameManager.GameStateManager.ChangeState(GameStateEnum.Planning);
        }
    }
}