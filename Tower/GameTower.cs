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
    private  List<Vector2> _wayPoints = new()
    {
        new Vector2(0, 0),
        new Vector2(100, 50),
        new Vector2(200, 150)
    };
    private BasicEnemy _enemy = new BasicEnemy(100, 5, 50, EnemyTypeEnum.Basic);
    private Citadel _citadel = new Citadel(0, 100, new Vector2(300, 300));
    private PathManager _pathManager;

    public GameTower()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _pathManager = new PathManager(_wayPoints, new Vector2(300, 300));
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
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _enemy.FollowPath(_pathManager, _citadel);
        Console.WriteLine($"Враг на позиции: {_enemy.Position}, Цитадель: {_citadel.Health} HP");
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