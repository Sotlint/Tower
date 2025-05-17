using System;
using Microsoft.Xna.Framework;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.LogicUpdaters;

public static class MainUpdateLogic
{
    public static void Update(GameTime gameTime)
    {
        var state = GameManager.GameStateManager.CurrentState;
        GameManager.InputManager.Update(gameTime);
        if (state is GameStateEnum.Playing)
        {
            WaveUpdateLogic.Update(gameTime);
            return;
        }
        if (state is GameStateEnum.Planning)
        {
            PlannedUpdateLogic.Update(gameTime);
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
            GameManager.GameStateManager.ChangeState(GameStateEnum.Menu);
        }
    }
}