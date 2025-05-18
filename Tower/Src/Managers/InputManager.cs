using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Tower.Core.Abstractions.Enums;

namespace Tower.Managers;

public class InputManager
{
    private KeyboardState _currentKeyboardState;
    private KeyboardState _previousKeyboardState;
    private MouseState _currentMouseState;
    private MouseState _previousMouseState;
    public Vector2 MousePosition { get; private set; }
    public MouseState CurrentMouseState => _currentMouseState;

    public bool LeftClick =>
        _previousMouseState.LeftButton == ButtonState.Released &&
        _currentMouseState.LeftButton == ButtonState.Pressed;

    public bool RightClick =>
        _previousMouseState.RightButton == ButtonState.Released;

    public void Update(GameTime gameTime)
    {
        _previousMouseState = _currentMouseState;
        _currentMouseState = Mouse.GetState();
        MousePosition = _currentMouseState.Position.ToVector2();

        // Получаем текущее состояние клавиатуры
        _previousKeyboardState = _currentKeyboardState;
        _currentKeyboardState = Keyboard.GetState();

        // Проверяем, была ли нажата клавиша ESC
        if (_currentKeyboardState.IsKeyDown(Keys.Escape) && _previousKeyboardState.IsKeyUp(Keys.Escape))
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