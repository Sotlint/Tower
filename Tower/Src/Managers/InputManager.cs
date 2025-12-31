using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Tower.Core.Abstractions.Enums;

namespace Tower.Managers;

/// <summary>
/// Менеджер ввода. Отслеживает состояние клавиатуры и мыши,
/// обрабатывает нажатия клавиш и клики мыши.
/// </summary>
public class InputManager
{
    /// <summary>Текущее состояние клавиатуры</summary>
    private KeyboardState _currentKeyboardState;
    
    /// <summary>Предыдущее состояние клавиатуры (для определения нажатий)</summary>
    private KeyboardState _previousKeyboardState;
    
    /// <summary>Текущее состояние мыши</summary>
    private MouseState _currentMouseState;
    
    /// <summary>Предыдущее состояние мыши (для определения кликов)</summary>
    private MouseState _previousMouseState;
    
    /// <summary>Текущая позиция курсора мыши в координатах экрана</summary>
    public Vector2 MousePosition { get; private set; }
    
    /// <summary>Текущее состояние мыши (для внешнего доступа)</summary>
    public MouseState CurrentMouseState => _currentMouseState;
    
    /// <summary>Флаг режима отладки (включается Ctrl+D)</summary>
    public bool IsDebugMode = false;

    /// <summary>
    /// Свойство, возвращающее true при нажатии левой кнопки мыши
    /// (переход из Released в Pressed)
    /// </summary>
    public bool LeftClick =>
        _previousMouseState.LeftButton == ButtonState.Released &&
        _currentMouseState.LeftButton == ButtonState.Pressed;

    /// <summary>
    /// Свойство, возвращающее true при отпускании левой кнопки мыши
    /// (переход из Pressed в Released). Используется для завершения перетаскивания.
    /// </summary>
    public bool LeftButtonReleased =>
        _previousMouseState.LeftButton == ButtonState.Pressed &&
        _currentMouseState.LeftButton == ButtonState.Released;

    /// <summary>
    /// Свойство, возвращающее true при нажатии правой кнопки мыши
    /// </summary>
    public bool RightClick =>
        _previousMouseState.RightButton == ButtonState.Released;

    /// <summary>
    /// Обновление состояния ввода. Вызывается каждый кадр.
    /// Сохраняет предыдущие состояния и получает текущие для определения событий.
    /// </summary>
    /// <param name="gameTime">Время игры (не используется, но требуется для совместимости)</param>
    public void Update(GameTime gameTime)
    {
        // Сохраняем предыдущее состояние мыши и получаем текущее
        _previousMouseState = _currentMouseState;
        _currentMouseState = Mouse.GetState();
        MousePosition = _currentMouseState.Position.ToVector2(); // Обновляем позицию курсора

        // Сохраняем предыдущее состояние клавиатуры и получаем текущее
        _previousKeyboardState = _currentKeyboardState;
        _currentKeyboardState = Keyboard.GetState();

        // Обработка нажатия клавиши ESC (пауза/возобновление игры)
        if (_currentKeyboardState.IsKeyDown(Keys.Escape) && _previousKeyboardState.IsKeyUp(Keys.Escape))
        {
            OnEscapePressed();
        }
        
        // Включение/выключение режима отладки (Ctrl + D)
        if (_currentKeyboardState.IsKeyDown(Keys.D) && _currentKeyboardState.IsKeyDown(Keys.LeftControl))
        {
            IsDebugMode = true; // Показываем границы объектов и радиусы атаки
        }
        else
        {
            IsDebugMode = false;
        }
    }

    /// <summary>
    /// Обработчик нажатия клавиши ESC. Переключает между паузой и игрой.
    /// Если игра на паузе - возобновляет, иначе ставит на паузу.
    /// </summary>
    private void OnEscapePressed()
    {
        var currentState = GameManager.GameStateManager.CurrentState;
        var previousState = GameManager.GameStateManager.PreviousState;

        // Если игра на паузе - возобновляем предыдущее состояние
        if (currentState == GameStateEnum.Paused)
        {
            GameManager.GameStateManager.ChangeState(previousState ?? GameStateEnum.Menu);
        }
        else
        {
            // Иначе ставим игру на паузу
            GameManager.GameStateManager.ChangeState(GameStateEnum.Paused);
        }
    }
}