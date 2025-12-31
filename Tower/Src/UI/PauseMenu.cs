using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.UI;

/// <summary>
/// Меню паузы. Отображается поверх игры при нажатии ESC.
/// Содержит кнопки для продолжения игры, возврата в главное меню и выхода.
/// </summary>
public class PauseMenu : BaseMenu
{
    /// <summary>Список кнопок меню паузы</summary>
    private readonly List<UIButton> _buttons = new();

    /// <summary>
    /// Конструктор меню паузы. Инициализирует полупрозрачный черный фон.
    /// </summary>
    public PauseMenu()
    {
        Position = Vector2.Zero;
        BackgroundColor = new Color(0, 0, 0, 180); // Полупрозрачный черный фон (альфа = 180)
        Initialize();
    }

    /// <summary>
    /// Инициализация меню. Размеры кнопок будут вычислены динамически при первом Draw.
    /// </summary>
    public override void Initialize()
    {
        // Размеры будут получены динамически при первом Draw
    }

    /// <summary>
    /// Инициализация кнопок меню паузы. Вычисляет позиции кнопок на основе размеров экрана
    /// и создает кнопки "Продолжить", "Главное меню" и "Выход".
    /// </summary>
    /// <param name="graphicsDevice">Графическое устройство для получения размеров экрана</param>
    private void InitializeButtons(GraphicsDevice graphicsDevice)
    {
        // Инициализация выполняется только один раз
        if (_buttons.Count > 0) return;
        
        // Получаем размеры экрана
        var screenWidth = graphicsDevice.Viewport.Width;
        var screenHeight = graphicsDevice.Viewport.Height;
        
        // Параметры кнопок
        var buttonWidth = 200; // Ширина кнопки
        var buttonHeight = 50; // Высота кнопки
        var buttonSpacing = 20; // Расстояние между кнопками
        var startY = screenHeight / 2 - 50; // Начальная позиция по Y (центр экрана - 50px)

        // Создание кнопки "Продолжить"
        var resumeButton = new UIButton(new Rectangle(
            screenWidth / 2 - buttonWidth / 2, // Центрирование по горизонтали
            startY,
            buttonWidth,
            buttonHeight
        ), "Продолжить")
        {
            OnClick = () =>
            {
                // Возвращаемся в предыдущее состояние (обычно Playing)
                var previousState = GameManager.GameStateManager.PreviousState;
                GameManager.GameStateManager.ChangeState(previousState ?? GameStateEnum.Playing);
            }
        };
        _buttons.Add(resumeButton);

        // Создание кнопки "Главное меню"
        var mainMenuButton = new UIButton(new Rectangle(
            screenWidth / 2 - buttonWidth / 2, // Центрирование по горизонтали
            startY + buttonHeight + buttonSpacing, // Позиция ниже первой кнопки
            buttonWidth,
            buttonHeight
        ), "Главное меню")
        {
            OnClick = () =>
            {
                // Переходим в главное меню
                GameManager.GameStateManager.ChangeState(GameStateEnum.Menu);
            }
        };
        _buttons.Add(mainMenuButton);

        // Создание кнопки "Выход"
        var exitButton = new UIButton(new Rectangle(
            screenWidth / 2 - buttonWidth / 2, // Центрирование по горизонтали
            startY + (buttonHeight + buttonSpacing) * 2, // Позиция ниже второй кнопки
            buttonWidth,
            buttonHeight
        ), "Выход")
        {
            OnClick = () =>
            {
                // TODO: Реализовать выход из игры
            }
        };
        _buttons.Add(exitButton);
    }

    /// <summary>
    /// Обновление состояния меню паузы. Обновляет hover эффекты на всех кнопках.
    /// </summary>
    /// <param name="gameTime">Время игры</param>
    /// <param name="mousePosition">Текущая позиция курсора мыши</param>
    public override void Update(GameTime gameTime, Vector2 mousePosition)
    {
        // Обновляем состояние каждой кнопки (hover эффекты)
        foreach (var button in _buttons)
        {
            button.Update(mousePosition);
        }
    }

    /// <summary>
    /// Обработка клика по меню паузы. Проверяет клики по всем кнопкам.
    /// </summary>
    /// <param name="mousePosition">Позиция курсора мыши при клике</param>
    public override void HandleClick(Vector2 mousePosition)
    {
        // Проверяем каждую кнопку на клик
        foreach (var button in _buttons)
        {
            if (button.HandleClick(mousePosition))
            {
                break; // Клик обработан, прекращаем проверку
            }
        }
    }

    /// <summary>
    /// Отрисовка содержимого меню паузы. Отрисовывает кнопки.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="gameTime">Время игры</param>
    protected override void DrawContent(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Инициализируем кнопки при первом Draw, когда GraphicsDevice доступен
        InitializeButtons(spriteBatch.GraphicsDevice);
        
        // Отрисовка заголовка "ПАУЗА"
        // TODO: Добавить текст

        // Отрисовка всех кнопок меню
        foreach (var button in _buttons)
        {
            button.Draw(spriteBatch);
        }
    }
}

