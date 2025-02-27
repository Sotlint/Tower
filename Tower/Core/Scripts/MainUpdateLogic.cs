using Microsoft.Xna.Framework;
using Tower.Core.Managers;

namespace Tower.Core.Scripts;

public static class MainUpdateLogic
{
    public static void Update(GameTime gameTime, GameManager gameManager)
    {
        PlannedUpdateLogic.Update(gameTime, gameManager);
        WaveUpdateLogic.Update(gameTime, gameManager);
    }
}