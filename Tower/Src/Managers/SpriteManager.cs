using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tower.Managers;

/// <summary>
/// Отвечает за загрузку и хранение текстур спрайтов, используемых в игре.
/// Предоставляет свойства для доступа к основным спрайтам: врагов, цитадели, башни и стены.
/// </summary>
public class SpriteManager
{
    public Texture2D BaseEnemySprite { get; set; }
    public Texture2D CitadelSprite { get; set; }
    public Texture2D TowerSprite { get; set; }
    public Texture2D WallSprite { get; set; }
    public Texture2D HealthBarSprite { get; set; }

    public void LoadSprites(ContentManager contentManager, GraphicsDevice graphicsDevice)
    {
        BaseEnemySprite = contentManager.Load<Texture2D>("monster");
        CitadelSprite = contentManager.Load<Texture2D>("citadel");
        TowerSprite = contentManager.Load<Texture2D>("tower");
        WallSprite = contentManager.Load<Texture2D>("wall");
        HealthBarSprite = new Texture2D(graphicsDevice, 1, 1);
        HealthBarSprite.SetData(new Color[] { Color.White });
    }
}