using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower.UI;

/// <summary>
/// Абстрактный базовый класс для всех меню в игре.
/// Предоставляет общую функциональность для обновления, отрисовки и обработки событий.
/// Все конкретные меню (MainMenu, PauseMenu, GameOverMenu) наследуются от этого класса.
/// </summary>
public abstract class BaseMenu
{
    /// <summary>Позиция меню на экране (используется для позиционирования элементов)</summary>
    protected Vector2 Position { get; set; }
    
    /// <summary>Текстура фона меню (опционально, если null - используется BackgroundColor)</summary>
    protected Texture2D BackgroundTexture { get; set; }
    
    /// <summary>Цвет фона меню (используется если BackgroundTexture = null)</summary>
    protected Color BackgroundColor { get; set; } = Color.Transparent;
    
    /// <summary>
    /// Обновление состояния меню. Вызывается каждый кадр для обработки ввода,
    /// анимаций и обновления состояния элементов меню.
    /// </summary>
    /// <param name="gameTime">Время игры для синхронизации</param>
    /// <param name="mousePosition">Текущая позиция курсора мыши</param>
    public virtual void Update(GameTime gameTime, Vector2 mousePosition)
    {
    }

    /// <summary>
    /// Отрисовка меню. Отрисовывает фон и вызывает DrawContent для отрисовки содержимого.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="gameTime">Время игры</param>
    public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        spriteBatch.Begin();
        
        // Отрисовка фона, если задана текстура
        if (BackgroundTexture != null)
        {
            var screenBounds = spriteBatch.GraphicsDevice.Viewport.Bounds;
            spriteBatch.Draw(BackgroundTexture, screenBounds, BackgroundColor);
        }
        // Или отрисовка фона цветом, если текстура не задана
        else if (BackgroundColor != Color.Transparent)
        {
            var screenBounds = spriteBatch.GraphicsDevice.Viewport.Bounds;
            // Создаем однопиксельную текстуру для заливки цветом
            var pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { BackgroundColor });
            spriteBatch.Draw(pixel, screenBounds, BackgroundColor);
        }
        
        // Отрисовка содержимого меню (кнопки, текст и т.д.)
        DrawContent(spriteBatch, gameTime);
        
        spriteBatch.End();
    }

    /// <summary>
    /// Отрисовка содержимого меню (кнопки, текст и т.д.).
    /// Должен быть реализован в наследниках для отрисовки конкретных элементов меню.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="gameTime">Время игры</param>
    protected abstract void DrawContent(SpriteBatch spriteBatch, GameTime gameTime);

    /// <summary>
    /// Обработка клика мыши по меню. Вызывается при клике для обработки взаимодействия
    /// с элементами меню (кнопками и т.д.).
    /// </summary>
    /// <param name="mousePosition">Позиция курсора мыши при клике</param>
    public virtual void HandleClick(Vector2 mousePosition)
    {
    }

    /// <summary>
    /// Инициализация меню. Вызывается при создании меню для подготовки элементов.
    /// Может быть переопределен в наследниках для дополнительной инициализации.
    /// </summary>
    public virtual void Initialize()
    {
    }
}

