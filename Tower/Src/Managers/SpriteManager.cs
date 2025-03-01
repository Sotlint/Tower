using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tower.Managers;

public class SpriteManager
{
    public Texture2D BaseEnemySprite { get; set; }

    public void LoadSprites(ContentManager contentManager)
    {
        BaseEnemySprite = contentManager.Load<Texture2D>("monster");
    }
}