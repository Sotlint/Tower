using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tower.Core.Managers;
using Tower.Core.Models;
using Tower.Core.Models.Abstractions;

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