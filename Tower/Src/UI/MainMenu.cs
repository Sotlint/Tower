using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.UI;

/// <summary>
/// Главное меню игры. Отображается при запуске игры и содержит
/// кнопки для начала новой игры и выхода.
/// </summary>
public class MainMenu : BaseMenu
{
    /// <summary>Список кнопок главного меню</summary>
    private readonly List<UIButton> _buttons = new();
    
    /// <summary>Шрифт для отрисовки текста (TODO: добавить загрузку)</summary>
    private SpriteFont _font; // TODO: Добавить загрузку шрифта

    /// <summary>
    /// Конструктор главного меню. Инициализирует фон и вызывает Initialize().
    /// </summary>
    public MainMenu()
    {
        Position = Vector2.Zero;
        BackgroundColor = new Color(30, 30, 40); // Темно-синий фон
        Initialize();
    }

    /// <summary>
    /// Инициализация меню. Размеры кнопок будут вычислены динамически при первом Draw,
    /// когда будет доступен GraphicsDevice для получения размеров экрана.
    /// </summary>
    public override void Initialize()
    {
        // Размеры будут получены динамически при первом Draw
    }

    /// <summary>
    /// Инициализация кнопок меню. Вычисляет позиции кнопок на основе размеров экрана
    /// и создает кнопки "Начать игру" и "Выход".
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
        var startY = screenHeight / 2 - 100; // Начальная позиция по Y (центр экрана - 100px)

        // Создание кнопки "Начать игру"
        var startButton = new UIButton(new Rectangle(
            screenWidth / 2 - buttonWidth / 2, // Центрирование по горизонтали
            startY,
            buttonWidth,
            buttonHeight
        ), "Начать игру")
        {
            OnClick = () =>
            {
                // При клике начинаем новую игру
                GameManager.StartNewGame();
            }
        };
        _buttons.Add(startButton);

        // Создание кнопки "Выход"
        var exitButton = new UIButton(new Rectangle(
            screenWidth / 2 - buttonWidth / 2, // Центрирование по горизонтали
            startY + buttonHeight + buttonSpacing, // Позиция ниже первой кнопки
            buttonWidth,
            buttonHeight
        ), "Выход")
        {
            OnClick = () =>
            {
                // Закрываем игру
                GameManager.ExitGame();
            }
        };
        _buttons.Add(exitButton);
    }

    /// <summary>
    /// Обновление состояния меню. Обновляет hover эффекты на всех кнопках.
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
    /// Обработка клика по меню. Проверяет клики по всем кнопкам.
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
    /// Отрисовка содержимого главного меню. Отрисовывает кнопки.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="gameTime">Время игры</param>
    protected override void DrawContent(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Инициализируем кнопки при первом Draw, когда GraphicsDevice доступен
        InitializeButtons(spriteBatch.GraphicsDevice);
        
        // Отрисовка заголовка (пока просто пустое место)
        // TODO: Добавить текст "TOWER DEFENSE" или логотип

        // Отрисовка всех кнопок меню
        foreach (var button in _buttons)
        {
            button.Draw(spriteBatch);
        }
    }
}

