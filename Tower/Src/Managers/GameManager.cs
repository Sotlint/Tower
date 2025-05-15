
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core;
using Tower.Core.Abstractions.Enums;
using Tower.Factories;

namespace Tower.Managers;

/// <summary>
/// Управление основными системами и состоянием игры, включая игрока, башни, врагов, цитадель, сложность и спрайты.
/// Ответственность за инициализацию и координацию основных менеджеров и отслеживание счета игрока.
/// </summary>
public class GameManager
{
    public Player Player { get; private set; }
    public GameStateManager GameStateManager { get; private set; }
    public TowerManager TowerManager { get; private set; }
    public CitadelManager CitadelManager { get; private set; }
    public EnemyManager EnemyManager { get; private set; }
    public DifficultyManager DifficultyManager { get; private set; }
    public SpriteManager SpriteManager { get; private set; }
    private int Score { get; set; }

    public void UpdateScore(int points)
    {
        Score += points;
    }

    public void Init(GraphicsDevice graphicsDevice)
    {
        Score = 0;
        TowerManager = new TowerManager();
        GameStateManager = new GameStateManager();
        EnemyManager = new EnemyManager();
        DifficultyManager = new DifficultyManager();
        SpriteManager = new SpriteManager();
        Player = new Player(100);
        var citadelPosition = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);
        var citadel = new Citadel(Guid.NewGuid(), citadelPosition, 100, 10, 200f, TimeSpan.Zero);
        CitadelManager = new CitadelManager(citadel);
    }

    public void TestConfig(Vector2 citadelPos)
    {
        var testTower = TowerFactory.CreateTower(TowerTypeEnum.Basic, citadelPos + new Vector2(-50, -50), SpriteManager);
        TowerManager.AddTower(testTower);
    }
}