using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower.UI;

/// <summary>
/// Абстрактный базовый класс для всех меню в игре.
/// Предоставляет общую функциональность для обновления, отрисовки и обработки событий.
/// </summary>
public abstract class BaseMenu
{
    protected Vector2 Position { get; set; }
    protected Texture2D BackgroundTexture { get; set; }
    protected Color BackgroundColor { get; set; } = Color.Transparent;
    
    /// <summary>
    /// Обновление состояния меню (обработка ввода, анимации и т.д.)
    /// </summary>
    public virtual void Update(GameTime gameTime, Vector2 mousePosition)
    {
    }

    /// <summary>
    /// Отрисовка меню
    /// </summary>
    public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        spriteBatch.Begin();
        
        // Отрисовка фона, если есть текстура
        if (BackgroundTexture != null)
        {
            var screenBounds = spriteBatch.GraphicsDevice.Viewport.Bounds;
            spriteBatch.Draw(BackgroundTexture, screenBounds, BackgroundColor);
        }
        // Или просто цвет фона
        else if (BackgroundColor != Color.Transparent)
        {
            var screenBounds = spriteBatch.GraphicsDevice.Viewport.Bounds;
            var pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { BackgroundColor });
            spriteBatch.Draw(pixel, screenBounds, BackgroundColor);
        }
        
        DrawContent(spriteBatch, gameTime);
        
        spriteBatch.End();
    }

    /// <summary>
    /// Отрисовка содержимого меню (кнопки, текст и т.д.)
    /// Переопределяется в наследниках
    /// </summary>
    protected abstract void DrawContent(SpriteBatch spriteBatch, GameTime gameTime);

    /// <summary>
    /// Обработка клика мыши
    /// </summary>
    public virtual void HandleClick(Vector2 mousePosition)
    {
    }

    /// <summary>
    /// Инициализация меню (вызывается при создании)
    /// </summary>
    public virtual void Initialize()
    {
    }
}

