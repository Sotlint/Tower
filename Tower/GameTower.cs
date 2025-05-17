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
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        GameManager.Init(_graphics.GraphicsDevice);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        SpriteManager.LoadSprites(Content, GraphicsDevice);
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        GameManager.CitadelManager.GetCitadel().SetSprite(SpriteManager.CitadelSprite);
        GameManager.TestConfig(GameManager.CitadelManager.GetCitadel().Position);
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