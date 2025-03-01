using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tower.Managers;

public class SpriteManager
{
    public Texture2D BaseEnemySprite { get; set; }
    public Texture2D CiradelSprite { get; set; }
    public Texture2D TowerSprite { get; set; }
    public Texture2D WallSprite { get; set; }

    public void LoadSprites(ContentManager contentManager)
    {
        BaseEnemySprite = contentManager.Load<Texture2D>("monster");
        CiradelSprite = contentManager.Load<Texture2D>("citadel");
        TowerSprite = contentManager.Load<Texture2D>("tower");
        WallSprite = contentManager.Load<Texture2D>("wall");
    }
}