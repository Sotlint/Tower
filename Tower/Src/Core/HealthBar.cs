using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;

namespace Tower.Core;

public class HealthBar
{
    public Texture2D Sprite { get; set; }

    private HealthBar()
    {
    }

    public HealthBar(Texture2D sprite)
    {
        Sprite = sprite;
    }


    public void Draw(SpriteBatch spriteBatch, GameTime gameTime, IEnemy enemy)
    {
        if (enemy.Health <= 0)
            return;

        var healthPercent = MathHelper.Clamp((float)enemy.Health / enemy.MaxHealth, 0, 1);

        var backColor = Color.DarkRed;
        var frontColor = Color.LimeGreen;

        var barPosition = enemy.Position + new Vector2(-20F, -20);

        spriteBatch.Draw(
            texture: Sprite,
            destinationRectangle: new Rectangle((int)barPosition.X, (int)barPosition.Y, (int)40, (int)10),
            color: backColor
        );

        spriteBatch.Draw(
            texture: Sprite,
            destinationRectangle: new Rectangle((int)barPosition.X, (int)barPosition.Y, (int)(40 * healthPercent),
                (int)10),
            color: frontColor
        );
    }
}