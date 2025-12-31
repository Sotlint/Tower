using Microsoft.Xna.Framework.Graphics;

namespace Tower.Core.Abstractions.Base;

/// <summary>
/// Интерфейс для объектов, которые имеют спрайт для отрисовки.
/// Реализуется всеми объектами, которые отрисовываются на экране.
/// </summary>
public interface IHaveSprite
{
    /// <summary>Масштаб спрайта (для изменения размера при отрисовке)</summary>
    float SpriteScale { get; }
    
    /// <summary>Текстура спрайта объекта</summary>
    Texture2D Sprite { get; }
}