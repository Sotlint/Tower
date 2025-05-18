using Microsoft.Xna.Framework;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.LogicUpdaters;

public static class PlannedUpdateLogic
{
    public static void Update(GameTime gameTime)
    {
        //GameManager.UIManager.TowerSelectionMenu.Update(GameManager.InputManager.MousePosition);
        
        GameManager.EnemyManager.SpawnEnemy(
            GameManager.DifficultyManager.GetEnemyCount());
       GameManager.GameStateManager.ChangeState(GameStateEnum.Playing);
    }
}