using System;
using Microsoft.Xna.Framework;
using Tower.Managers;
using Tower.Models.Abstractions;

namespace Tower.LogicUpdaters;

public static class MainUpdateLogic
{
    public static void Update(GameTime gameTime, GameManager gameManager)
    {
        var state = gameManager.GameStateManager.CurrentState;
        if (state is GameStateEnum.Playing)
        {
            WaveUpdateLogic.Update(gameTime, gameManager);
            return;
        }
        if (state is GameStateEnum.Planning)
        {
            PlannedUpdateLogic.Update(gameTime, gameManager);
            return;
        }
        if (state is GameStateEnum.Menu)
        {
            return;
        }
        if (state is GameStateEnum.Paused)
        {
            //ignore
            return;
        }
        if (state == GameStateEnum.GameOver)
        {
            Console.WriteLine("Game over!");
            gameManager.GameStateManager.ChangeState(GameStateEnum.Menu);
            return;
        }
    }
}