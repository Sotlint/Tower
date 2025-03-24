using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower.Core.Abstractions.Base;

public interface IHaveDrawLogic : IHaveSprite
{
    void Draw(SpriteBatch spriteBatch, GameTime gameTime);
}