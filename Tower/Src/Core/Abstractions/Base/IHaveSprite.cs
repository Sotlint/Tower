using Microsoft.Xna.Framework.Graphics;

namespace Tower.Core.Abstractions.Base;

public interface IHaveSprite
{
    float SpriteScale { get; }
    Texture2D Sprite { get; }
}