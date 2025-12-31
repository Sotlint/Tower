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
            var menu = GameManager.UIManager.GetActiveMenu();
            menu?.Update(gameTime, GameManager.InputManager.MousePosition);
            
            if (GameManager.InputManager.LeftClick)
            {
                menu?.HandleClick(GameManager.InputManager.MousePosition);
            }
            return;
        }
        if (state is GameStateEnum.Paused)
        {
            var menu = GameManager.UIManager.GetActiveMenu();
            menu?.Update(gameTime, GameManager.InputManager.MousePosition);
            
            if (GameManager.InputManager.LeftClick)
            {
                menu?.HandleClick(GameManager.InputManager.MousePosition);
            }
            return;
        }
        if (state == GameStateEnum.GameOver)
        {
            var menu = GameManager.UIManager.GetActiveMenu();
            menu?.Update(gameTime, GameManager.InputManager.MousePosition);
            
            if (GameManager.InputManager.LeftClick)
            {
                menu?.HandleClick(GameManager.InputManager.MousePosition);
            }
            return;
        }
    }
}