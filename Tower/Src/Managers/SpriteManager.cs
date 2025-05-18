using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tower.Managers;

/// <summary>
/// Отвечает за загрузку и хранение текстур спрайтов, используемых в игре.
/// Предоставляет свойства для доступа к основным спрайтам: врагов, цитадели, башни и стены.
/// </summary>
public static class SpriteManager
{
    public static Texture2D BaseEnemySprite { get; private set; }
    public static Texture2D CitadelSprite { get; private set; }
    public static Texture2D TowerSprite { get; private set; }
    public static Texture2D WallSprite { get; private set; }
    public static Texture2D HealthBarSprite { get; private set; }
    public static Texture2D OrbProjectileSprite { get; private set; }
    public static Texture2D ButtonBackgroundTexture { get; private set; }

    public static void LoadSprites(ContentManager contentManager, GraphicsDevice graphicsDevice)
    {
        BaseEnemySprite = contentManager.Load<Texture2D>("monster");
        CitadelSprite = contentManager.Load<Texture2D>("citadel");
        TowerSprite = contentManager.Load<Texture2D>("tower");
        WallSprite = contentManager.Load<Texture2D>("wall");
        HealthBarSprite = CreateHealthBarTexture(graphicsDevice);
        OrbProjectileSprite = CreateCircularTexture(graphicsDevice, 8, Color.Blue);
        ButtonBackgroundTexture = CreateButtonBackgroundTexture(graphicsDevice);
    }


    private static Texture2D CreateButtonBackgroundTexture(GraphicsDevice graphicsDevice)
    {
        var texture = new Texture2D(graphicsDevice, 64, 64);
        var data = new Color[64 * 64];

        for (var i = 0; i < data.Length; i++)
        {
            data[i] = new Color(200, 200, 200);
        }

        texture.SetData(data);
        return texture;
    }

    private static Texture2D CreateHealthBarTexture(GraphicsDevice graphicsDevice)
    {
        var texture = new Texture2D(graphicsDevice, 1, 1);
        texture.SetData(new[] { Color.White });
        return texture;
    }

    private static Texture2D CreateCircularTexture(GraphicsDevice graphicsDevice, int radius, Color color)
    {
        var diameter = radius * 2;
        var texture = new Texture2D(graphicsDevice, diameter, diameter);
        var data = new Color[diameter * diameter];

        for (var y = 0; y < diameter; y++)
        {
            for (var x = 0; x < diameter; x++)
            {
                var dx = x - radius;
                var dy = y - radius;
                float distanceSquared = dx * dx + dy * dy;

                if (distanceSquared <= radius * radius)
                {
                    data[y * diameter + x] = color;
                }
                else
                {
                    data[y * diameter + x] = Color.Transparent;
                }
            }
        }

        texture.SetData(data);
        return texture;
    }
}