using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.UI;

/// <summary>
/// Меню окончания игры
/// </summary>
public class GameOverMenu : BaseMenu
{
    private readonly List<UIButton> _buttons = new();
    private int _finalScore;
    private int _finalWave;

    public GameOverMenu()
    {
        Position = Vector2.Zero;
        BackgroundColor = new Color(100, 0, 0, 200); // Полупрозрачный темно-красный фон
        Initialize();
    }

    public override void Initialize()
    {
        // Размеры будут получены динамически при первом Draw
    }

    private void InitializeButtons(GraphicsDevice graphicsDevice)
    {
        if (_buttons.Count > 0) return; // Уже инициализировано
        
        var screenWidth = graphicsDevice.Viewport.Width;
        var screenHeight = graphicsDevice.Viewport.Height;
        var buttonWidth = 200;
        var buttonHeight = 50;
        var buttonSpacing = 20;
        var startY = screenHeight / 2 + 50;

        // Кнопка "Повторить"
        var restartButton = new UIButton(new Rectangle(
            screenWidth / 2 - buttonWidth / 2,
            startY,
            buttonWidth,
            buttonHeight
        ), "Повторить")
        {
            OnClick = () =>
            {
                // TODO: Реализовать перезапуск игры
                GameManager.GameStateManager.ChangeState(GameStateEnum.Menu);
            }
        };
        _buttons.Add(restartButton);

        // Кнопка "Главное меню"
        var mainMenuButton = new UIButton(new Rectangle(
            screenWidth / 2 - buttonWidth / 2,
            startY + buttonHeight + buttonSpacing,
            buttonWidth,
            buttonHeight
        ), "Главное меню")
        {
            OnClick = () =>
            {
                GameManager.GameStateManager.ChangeState(GameStateEnum.Menu);
            }
        };
        _buttons.Add(mainMenuButton);

        // Кнопка "Выход"
        var exitButton = new UIButton(new Rectangle(
            screenWidth / 2 - buttonWidth / 2,
            startY + (buttonHeight + buttonSpacing) * 2,
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

    public void SetGameStats(int score, int wave)
    {
        _finalScore = score;
        _finalWave = wave;
    }

    public override void Update(GameTime gameTime, Vector2 mousePosition)
    {
        foreach (var button in _buttons)
        {
            button.Update(mousePosition);
        }
    }

    public override void HandleClick(Vector2 mousePosition)
    {
        foreach (var button in _buttons)
        {
            if (button.HandleClick(mousePosition))
            {
                break;
            }
        }
    }

    protected override void DrawContent(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Инициализируем кнопки при первом Draw, когда GraphicsDevice доступен
        InitializeButtons(spriteBatch.GraphicsDevice);
        
        // Отрисовка заголовка "ИГРА ОКОНЧЕНА"
        // TODO: Добавить текст

        // Отрисовка статистики
        // TODO: Добавить отображение счета и волны (_finalScore, _finalWave)

        // Отрисовка кнопок
        foreach (var button in _buttons)
        {
            button.Draw(spriteBatch);
        }
    }
}

