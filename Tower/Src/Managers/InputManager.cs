using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Tower.Core.Abstractions.Enums;

namespace Tower.Managers;

public class InputManager
{
    private KeyboardState currentKeyboardState;
    private KeyboardState previousKeyboardState;

    public void Update(GameTime gameTime)
    {
        // Получаем текущее состояние клавиатуры
        previousKeyboardState = currentKeyboardState;
        currentKeyboardState = Keyboard.GetState();

        // Проверяем, была ли нажата клавиша ESC
        if (currentKeyboardState.IsKeyDown(Keys.Escape) && previousKeyboardState.IsKeyUp(Keys.Escape))
        {
            OnEscapePressed();
        }
    }

    private void OnEscapePressed()
    {
        var currentState = GameManager.GameStateManager.CurrentState;
        var previousState = GameManager.GameStateManager.PreviousState;

        if (currentState == GameStateEnum.Paused)
        {
            GameManager.GameStateManager.ChangeState(previousState ?? GameStateEnum.Menu);
        }
        else
        {
            GameManager.GameStateManager.ChangeState(GameStateEnum.Paused);
        }
    }
}