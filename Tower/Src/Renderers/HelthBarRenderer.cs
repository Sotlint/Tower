using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower.Renderers;

public static class HealthBarRenderer
{
    private static Texture2D _whiteTexture;
    public static void Draw(SpriteBatch spriteBatch, Vector2 position, int currentHealth, int maxHealth, float width = 40, float height = 6)
    {
        if (maxHealth <= 0) return;
        float healthPercent = MathHelper.Clamp((float)currentHealth / maxHealth, 0, 1);

        Color backColor = Color.DarkRed;
        Color frontColor = Color.LimeGreen;

        Vector2 barPosition = position + new Vector2(-width / 2, -20);

        spriteBatch.Draw(
            texture: GetWhiteTexture(spriteBatch.GraphicsDevice),
            destinationRectangle: new Rectangle((int)barPosition.X, (int)barPosition.Y, (int)width, (int)height),
            color: backColor
        );

        spriteBatch.Draw(
            texture: GetWhiteTexture(spriteBatch.GraphicsDevice),
            destinationRectangle: new Rectangle((int)barPosition.X, (int)barPosition.Y, (int)(width * healthPercent), (int)height),
            color: frontColor
        );
    }

    private static Texture2D GetWhiteTexture(GraphicsDevice graphicsDevice)
    {
        if (_whiteTexture == null)
        {
            _whiteTexture = new Texture2D(graphicsDevice, 1, 1);
            _whiteTexture.SetData(new[] { Color.White });
        }
        return _whiteTexture;
    }
}