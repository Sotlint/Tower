using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.UI;

public class TowerSelectionMenu
{
    private readonly List<UIButton<TowerTypeEnum>> _towerButtons = new();
    private Rectangle _menuBounds;
    private Rectangle _borderBounds;
    private bool _isInitialized = false;

    public Texture2D BackgroundTexture { get; set; }
    public Texture2D FrameTexture { get; set; }

    public TowerSelectionMenu()
    {
        // Инициализация будет выполнена при первом Draw
    }

    private void Initialize(GraphicsDevice graphicsDevice)
    {
        if (_isInitialized) return;
        
        var buttonSize = 64;
        var padding = 10;
        var borderThickness = 4;
        var types = new[] { TowerTypeEnum.Basic /*, TowerTypeEnum.Fire, etc. */ };
        
        var screenWidth = graphicsDevice.Viewport.Width;
        var screenHeight = graphicsDevice.Viewport.Height;
        
        // Вычисляем размеры меню
        var menuWidth = types.Length * buttonSize + (types.Length + 1) * padding;
        var menuHeight = buttonSize + padding * 2;
        var menuX = (screenWidth - menuWidth) / 2; // Центрируем по горизонтали
        var menuY = screenHeight - menuHeight - 20; // Снизу с отступом 20px
        
        _menuBounds = new Rectangle(menuX, menuY, menuWidth, menuHeight);
        _borderBounds = new Rectangle(
            menuX - borderThickness, 
            menuY - borderThickness, 
            menuWidth + borderThickness * 2, 
            menuHeight + borderThickness * 2
        );

        // Создаем кнопки горизонтально
        for (var i = 0; i < types.Length; i++)
        {
            var type = types[i];
            var x = menuX + padding + i * (buttonSize + padding);
            var y = menuY + padding;
            var bounds = new Rectangle(x, y, buttonSize, buttonSize);

            var button = new UIButton<TowerTypeEnum>(bounds, type)
            {
                IconTexture = SpriteManager.TowerSprite,
            };

            _towerButtons.Add(button);
        }
        
        _isInitialized = true;
    }

    public void Update(Vector2 mousePosition)
    {
        foreach (var button in _towerButtons)
        {
            button.Update(mousePosition);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Initialize(spriteBatch.GraphicsDevice);
        
        spriteBatch.Begin();
        
        // Отрисовка окантовки (рамки)
        var borderColor = Color.DarkGray;
        var borderTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        borderTexture.SetData(new[] { borderColor });
        
        // Верхняя и нижняя линии
        spriteBatch.Draw(borderTexture, new Rectangle(_borderBounds.X, _borderBounds.Y, _borderBounds.Width, 4), borderColor);
        spriteBatch.Draw(borderTexture, new Rectangle(_borderBounds.X, _borderBounds.Bottom - 4, _borderBounds.Width, 4), borderColor);
        
        // Левая и правая линии
        spriteBatch.Draw(borderTexture, new Rectangle(_borderBounds.X, _borderBounds.Y, 4, _borderBounds.Height), borderColor);
        spriteBatch.Draw(borderTexture, new Rectangle(_borderBounds.Right - 4, _borderBounds.Y, 4, _borderBounds.Height), borderColor);
        
        // Фон меню
        var backgroundColor = new Color(50, 50, 50, 200); // Полупрозрачный темный фон
        var backgroundTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        backgroundTexture.SetData(new[] { backgroundColor });
        spriteBatch.Draw(backgroundTexture, _menuBounds, backgroundColor);
        
        // Отрисовка кнопок
        foreach (var button in _towerButtons)
        {
            button.Draw(spriteBatch);
        }
        
        spriteBatch.End();
    }

    public TowerTypeEnum? GetTowerTypeAt(Vector2 mousePosition)
    {
        foreach (var button in _towerButtons)
        {
            if (button.Bounds.Contains(mousePosition))
                return button.Payload;
        }

        return null;
    }
    
    /// <summary>
    /// Получить границы меню для проверки коллизий
    /// </summary>
    public Rectangle? GetMenuBounds()
    {
        return _isInitialized ? _menuBounds : null;
    }
}