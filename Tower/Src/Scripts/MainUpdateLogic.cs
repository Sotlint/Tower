using System;
using Microsoft.Xna.Framework;
using Tower.Src.Managers;
using Tower.Src.Models.Abstractions;

namespace Tower.Src.Scripts;

public static class MainUpdateLogic
{
    public static void Update(GameTime gameTime, GameManager gameManager)
    {
        var state = gameManager.GameStateManager.CurrentState;
        if (state is GameStateEnum.Playing)
        {
            WaveUpdateLogic.Update(gameTime, gameManager);
        }
        if (state is GameStateEnum.Planning)
        {
            PlannedUpdateLogic.Update(gameTime, gameManager);
        }
        if (state is GameStateEnum.Menu)
        {
        }
        if (state is GameStateEnum.Paused)
        {
        }
        if (state is GameStateEnum.GameOver)
        {
            Console.WriteLine("Game over!");
        }
    }
}