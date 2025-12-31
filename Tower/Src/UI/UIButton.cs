using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Managers;

namespace Tower.UI;

/// <summary>
/// Универсальная кнопка UI с поддержкой hover эффектов и текста.
/// Используется для создания интерактивных элементов интерфейса.
/// </summary>
public class UIButton
{
    /// <summary>Границы кнопки на экране (позиция и размер)</summary>
    public Rectangle Bounds { get; private set; }
    
    /// <summary>Текст, отображаемый на кнопке</summary>
    public string Text { get; set; } = string.Empty;
    
    /// <summary>Текстура фона кнопки</summary>
    private Texture2D BackgroundTexture { get; set; }
    
    /// <summary>Текстура иконки кнопки (опционально)</summary>
    public Texture2D IconTexture { get; set; }
    
    /// <summary>Флаг, указывающий, находится ли курсор мыши над кнопкой</summary>
    public bool IsHovered { get; private set; }
    
    /// <summary>Флаг, указывающий, активна ли кнопка (можно ли с ней взаимодействовать)</summary>
    public bool IsEnabled { get; set; } = true;
    
    /// <summary>Цвет кнопки в нормальном состоянии</summary>
    public Color NormalColor { get; set; } = Color.White;
    
    /// <summary>Цвет кнопки при наведении курсора (hover)</summary>
    public Color HoverColor { get; set; } = Color.LightGray;
    
    /// <summary>Цвет кнопки в отключенном состоянии</summary>
    public Color DisabledColor { get; set; } = Color.Gray;
    
    /// <summary>Callback функция, вызываемая при клике на кнопку</summary>
    public System.Action OnClick { get; set; }

    /// <summary>
    /// Конструктор кнопки с текстом
    /// </summary>
    /// <param name="bounds">Границы кнопки (позиция и размер)</param>
    /// <param name="text">Текст кнопки</param>
    public UIButton(Rectangle bounds, string text = "")
    {
        Bounds = bounds;
        Text = text;
        BackgroundTexture = SpriteManager.ButtonBackgroundTexture;
    }

    /// <summary>
    /// Конструктор кнопки с иконкой и текстом
    /// </summary>
    /// <param name="bounds">Границы кнопки (позиция и размер)</param>
    /// <param name="iconTexture">Текстура иконки</param>
    /// <param name="text">Текст кнопки</param>
    public UIButton(Rectangle bounds, Texture2D iconTexture, string text = "")
    {
        Bounds = bounds;
        IconTexture = iconTexture;
        Text = text;
        BackgroundTexture = SpriteManager.ButtonBackgroundTexture;
    }

    /// <summary>
    /// Обновление состояния кнопки. Проверяет, находится ли курсор над кнопкой.
    /// </summary>
    /// <param name="mousePosition">Текущая позиция курсора мыши</param>
    public void Update(Vector2 mousePosition)
    {
        // Кнопка считается "наведенной", если она активна и курсор внутри границ
        IsHovered = IsEnabled && Bounds.Contains(mousePosition);
    }

    /// <summary>
    /// Обработка клика по кнопке. Вызывает callback, если клик был внутри границ кнопки.
    /// </summary>
    /// <param name="mousePosition">Позиция курсора мыши при клике</param>
    /// <returns>true, если клик был обработан, false - если клик был вне кнопки</returns>
    public bool HandleClick(Vector2 mousePosition)
    {
        // Проверяем, что кнопка активна и клик был внутри границ
        if (IsEnabled && Bounds.Contains(mousePosition))
        {
            OnClick?.Invoke(); // Вызываем callback, если он установлен
            return true;
        }
        return false;
    }

    /// <summary>
    /// Отрисовка кнопки. Отрисовывает фон, иконку и текст.
    /// Требует, чтобы spriteBatch.Begin() был вызван перед вызовом этого метода.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    public void Draw(SpriteBatch spriteBatch)
    {
        // Определяем цвет кнопки в зависимости от состояния
        var currentColor = !IsEnabled ? DisabledColor : (IsHovered ? HoverColor : NormalColor);
        
        // Отрисовка фона кнопки
        if (BackgroundTexture != null)
        {
            spriteBatch.Draw(BackgroundTexture, Bounds, currentColor);
        }
        
        // Отрисовка иконки, если она задана
        if (IconTexture != null)
        {
            spriteBatch.Draw(IconTexture, Bounds, currentColor);
        }
        
        // Отрисовка текста, если он задан и шрифт загружен
        if (!string.IsNullOrEmpty(Text) && SpriteManager.DefaultFont != null)
        {
            // Вычисляем размер текста для центрирования
            var textSize = SpriteManager.DefaultFont.MeasureString(Text);
            // Вычисляем позицию текста для центрирования в кнопке
            var textPosition = new Vector2(
                Bounds.X + (Bounds.Width - textSize.X) / 2, // Центрирование по горизонтали
                Bounds.Y + (Bounds.Height - textSize.Y) / 2 // Центрирование по вертикали
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
