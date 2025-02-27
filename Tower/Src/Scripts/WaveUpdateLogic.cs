using Microsoft.Xna.Framework;
using Tower.Src.Managers;
using Tower.Src.Models.Abstractions;

namespace Tower.Src.Scripts;

public static class WaveUpdateLogic
{
    public static void Update(GameTime gameTime, GameManager gameManager)
    {
       var enemies = gameManager.EnemyManager.GetEnemies();
       var citadel = gameManager.CitadelManager.GetCitadel();
       foreach (var enemy in enemies)
       {
           enemy.Update(citadel);
           if (citadel.IsDestroyed())
           {
               gameManager.GameStateManager.ChangeState(GameStateEnum.GameOver);
               break;
           }
       }
    }
}