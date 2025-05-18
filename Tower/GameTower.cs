using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.LogicUpdaters;
using Tower.Managers;
using Tower.Renderers;

// ReSharper disable PossibleLossOfFraction

namespace Tower;

public class GameTower : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public GameTower()
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics = new GraphicsDeviceManager(this);
       
    }

    protected override void Initialize()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        SpriteManager.LoadSprites(Content, GraphicsDevice);
        GameManager.Init(_graphics.GraphicsDevice);
        GameManager.CitadelManager.GetCitadel().SetSprite(SpriteManager.CitadelSprite);
        GameManager.TestConfig(GameManager.CitadelManager.GetCitadel().Position);
        base.Initialize();
    }

    protected override void LoadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
        MainUpdateLogic.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        MainRenderer.Render(_spriteBatch, _graphics, gameTime);
        base.Draw(gameTime);
    }
}