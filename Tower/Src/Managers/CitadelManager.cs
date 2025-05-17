using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core;

namespace Tower.Managers;

public class CitadelManager
{
    public CitadelManager(Citadel citadel)
    {
        Citadel = citadel;
    }

    private Citadel Citadel { get; set; }

    public void UpdateCitadel(GameTime gameTime)
        => Citadel.Update(gameTime);

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        => Citadel.Draw(spriteBatch, gameTime);

    public Citadel GetCitadel()
        => Citadel;
}