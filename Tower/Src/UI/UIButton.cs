using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Managers;

namespace Tower.UI;

public class UIButton<T>
{
    public Rectangle Bounds { get; private set; }
    public T Payload { get; private set; }
    private Texture2D BackgroundTexture { get; set; }
    public Texture2D IconTexture { get; set; }

    public bool IsHovered { get; private set; }

    public UIButton(Rectangle bounds, T payload)
    {
        Bounds = bounds;
        Payload = payload;
        BackgroundTexture = SpriteManager.ButtonBackgroundTexture;
    }

    public void Update(Vector2 mousePosition)
    {
        IsHovered = Bounds.Contains(mousePosition);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(BackgroundTexture, Bounds, Color.White);
        spriteBatch.Draw(IconTexture, Bounds, Color.White);
        spriteBatch.End();
    }
}