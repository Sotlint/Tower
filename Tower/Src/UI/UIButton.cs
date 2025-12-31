using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Managers;

namespace Tower.UI;

/// <summary>
/// Универсальная кнопка UI с поддержкой hover эффектов и текста
/// </summary>
public class UIButton
{
    public Rectangle Bounds { get; private set; }
    public string Text { get; set; } = string.Empty;
    private Texture2D BackgroundTexture { get; set; }
    public Texture2D IconTexture { get; set; }
    public bool IsHovered { get; private set; }
    public bool IsEnabled { get; set; } = true;
    
    // Цвета для разных состояний
    public Color NormalColor { get; set; } = Color.White;
    public Color HoverColor { get; set; } = Color.LightGray;
    public Color DisabledColor { get; set; } = Color.Gray;
    
    // Callback при клике
    public System.Action OnClick { get; set; }

    public UIButton(Rectangle bounds, string text = "")
    {
        Bounds = bounds;
        Text = text;
        BackgroundTexture = SpriteManager.ButtonBackgroundTexture;
    }

    public UIButton(Rectangle bounds, Texture2D iconTexture, string text = "")
    {
        Bounds = bounds;
        IconTexture = iconTexture;
        Text = text;
        BackgroundTexture = SpriteManager.ButtonBackgroundTexture;
    }

    public void Update(Vector2 mousePosition)
    {
        IsHovered = IsEnabled && Bounds.Contains(mousePosition);
    }

    public bool HandleClick(Vector2 mousePosition)
    {
        if (IsEnabled && Bounds.Contains(mousePosition))
        {
            OnClick?.Invoke();
            return true;
        }
        return false;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var currentColor = !IsEnabled ? DisabledColor : (IsHovered ? HoverColor : NormalColor);
        
        // Отрисовка фона кнопки
        if (BackgroundTexture != null)
        {
            spriteBatch.Draw(BackgroundTexture, Bounds, currentColor);
        }
        
        // Отрисовка иконки, если есть
        if (IconTexture != null)
        {
            spriteBatch.Draw(IconTexture, Bounds, currentColor);
        }
        
        // Отрисовка текста, если есть
        if (!string.IsNullOrEmpty(Text) && SpriteManager.DefaultFont != null)
        {
            var textSize = SpriteManager.DefaultFont.MeasureString(Text);
            var textPosition = new Vector2(
                Bounds.X + (Bounds.Width - textSize.X) / 2,
                Bounds.Y + (Bounds.Height - textSize.Y) / 2
            );
            spriteBatch.DrawString(SpriteManager.DefaultFont, Text, textPosition, currentColor);
        }
    }
}

/// <summary>
/// Generic версия кнопки для хранения данных определенного типа
/// </summary>
public class UIButton<T> : UIButton
{
    public T Payload { get; private set; }

    public UIButton(Rectangle bounds, T payload, string text = "") : base(bounds, text)
    {
        Payload = payload;
    }

    public UIButton(Rectangle bounds, T payload, Texture2D iconTexture, string text = "") 
        : base(bounds, iconTexture, text)
    {
        Payload = payload;
    }
}
