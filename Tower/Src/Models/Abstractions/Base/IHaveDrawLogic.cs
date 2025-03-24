using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower.Models.Abstractions.Base;

public interface IHaveDrawLogic
{
    Texture2D Sprite { get; }
    void Draw(SpriteBatch spriteBatch, GameTime gameTime);
}