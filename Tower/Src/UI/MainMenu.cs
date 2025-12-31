using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.UI;

/// <summary>
/// Главное меню игры
/// </summary>
public class MainMenu : BaseMenu
{
    private readonly List<UIButton> _buttons = new();
    private SpriteFont _font; // TODO: Добавить загрузку шрифта

    public MainMenu()
    {
        Position = Vector2.Zero;
        BackgroundColor = new Color(30, 30, 40); // Темно-синий фон
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
        var startY = screenHeight / 2 - 100;

        // Кнопка "Начать игру"
        var startButton = new UIButton(new Rectangle(
            screenWidth / 2 - buttonWidth / 2,
            startY,
            buttonWidth,
            buttonHeight
        ), "Начать игру")
        {
            OnClick = () =>
            {
                GameManager.StartNewGame();
            }
        };
        _buttons.Add(startButton);

        // Кнопка "Выход"
        var exitButton = new UIButton(new Rectangle(
            screenWidth / 2 - buttonWidth / 2,
            startY + buttonHeight + buttonSpacing,
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
        
        // Отрисовка заголовка (пока просто пустое место)
        // TODO: Добавить текст "TOWER DEFENSE" или логотип

        // Отрисовка кнопок
        foreach (var button in _buttons)
        {
            button.Draw(spriteBatch);
        }
    }
}

