using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.LogicUpdaters;
using Tower.Managers;
using Tower.Models;
using Tower.Renderers;

// ReSharper disable PossibleLossOfFraction

namespace Tower;

public class GameTower : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameManager _gameManager;

    public GameTower()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        var gameManager = new GameManager();
        gameManager.Init(_graphics.GraphicsDevice);
        _gameManager = gameManager;
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _gameManager.SpriteManager.LoadSprites(Content);
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _gameManager.CitadelManager.GetCitadel().SetSprite(_gameManager.SpriteManager.CiradelSprite);
    }

    protected override void Update(GameTime gameTime)
    {
        MainUpdateLogic.Update(gameTime, _gameManager);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        MainRenderer.Render(_gameManager, _spriteBatch, _graphics);
        base.Draw(gameTime);
    }
}