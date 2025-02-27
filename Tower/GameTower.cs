using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Managers;
using Tower.Core.Scripts;

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
        gameManager.Init();
        _gameManager = gameManager;
        // Add your initialization logic here
        base.Initialize();

    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        //use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        MainUpdateLogic.Update(gameTime, _gameManager);
        // Add your update logic here
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        // Add your drawing code here
        base.Draw(gameTime);
    }
}