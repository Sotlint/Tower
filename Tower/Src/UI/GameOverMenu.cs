using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.UI;

/// <summary>
/// Меню окончания игры. Отображается когда цитадель уничтожена.
/// Показывает финальную статистику и содержит кнопки для повтора, возврата в меню и выхода.
/// </summary>
public class GameOverMenu : BaseMenu
{
    /// <summary>Список кнопок меню окончания игры</summary>
    private readonly List<UIButton> _buttons = new();
    
    /// <summary>Финальный счет игрока (устанавливается при окончании игры)</summary>
    private int _finalScore;
    
    /// <summary>Номер последней пройденной волны (устанавливается при окончании игры)</summary>
    private int _finalWave;

    /// <summary>
    /// Конструктор меню окончания игры. Инициализирует полупрозрачный темно-красный фон.
    /// </summary>
    public GameOverMenu()
    {
        Position = Vector2.Zero;
        BackgroundColor = new Color(100, 0, 0, 200); // Полупрозрачный темно-красный фон (альфа = 200)
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
    /// Инициализация кнопок меню окончания игры. Вычисляет позиции кнопок на основе размеров экрана
    /// и создает кнопки "Повторить", "Главное меню" и "Выход".
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
        var startY = screenHeight / 2 + 50; // Начальная позиция по Y (центр экрана + 50px)

        // Создание кнопки "Повторить"
        var restartButton = new UIButton(new Rectangle(
            screenWidth / 2 - buttonWidth / 2, // Центрирование по горизонтали
            startY,
            buttonWidth,
            buttonHeight
        ), "Повторить")
        {
            OnClick = () =>
            {
                // TODO: Реализовать перезапуск игры
                // Пока просто возвращаемся в главное меню
                GameManager.GameStateManager.ChangeState(GameStateEnum.Menu);
            }
        };
        _buttons.Add(restartButton);

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
    /// Установка финальной статистики игры. Вызывается при переходе в состояние GameOver.
    /// </summary>
    /// <param name="score">Финальный счет игрока</param>
    /// <param name="wave">Номер последней пройденной волны</param>
    public void SetGameStats(int score, int wave)
    {
        _finalScore = score;
        _finalWave = wave;
    }

    /// <summary>
    /// Обновление состояния меню окончания игры. Обновляет hover эффекты на всех кнопках.
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
    /// Обработка клика по меню окончания игры. Проверяет клики по всем кнопкам.
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
    /// Отрисовка содержимого меню окончания игры. Отрисовывает статистику и кнопки.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="gameTime">Время игры</param>
    protected override void DrawContent(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Инициализируем кнопки при первом Draw, когда GraphicsDevice доступен
        InitializeButtons(spriteBatch.GraphicsDevice);
        
        // Отрисовка заголовка "ИГРА ОКОНЧЕНА"
        // TODO: Добавить текст

        // Отрисовка статистики (счет и номер волны)
        // TODO: Добавить отображение счета и волны (_finalScore, _finalWave)

        // Отрисовка всех кнопок меню
        foreach (var button in _buttons)
        {
            button.Draw(spriteBatch);
        }
    }
}

