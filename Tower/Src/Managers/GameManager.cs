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
public static class GameManager
{
    public static Player Player { get; private set; }
    public static GameStateManager GameStateManager { get; private set; }
    public static TowerManager TowerManager { get; private set; }
    public static CitadelManager CitadelManager { get; private set; }
    public static EnemyManager EnemyManager { get; private set; }
    public static DifficultyManager DifficultyManager { get; private set; }
    
    public static ProjectileManager ProjectileManager { get; private set; }
    private static int Score { get; set; }

    public static void UpdateScore(int points)
    {
        Score += points;
    }

    public static void Init(GraphicsDevice graphicsDevice)
    {
        Score = 0;
        TowerManager = new TowerManager();
        GameStateManager = new GameStateManager();
        EnemyManager = new EnemyManager();
        DifficultyManager = new DifficultyManager();
        ProjectileManager = new ProjectileManager();
        Player = new Player(100);
        
        var citadelHealBarSprite = new Texture2D(graphicsDevice, 1, 1);
        citadelHealBarSprite.SetData(new[] { Color.White });
        var citadelPosition = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);
        var citadel = new Citadel(Guid.NewGuid(), citadelPosition, 100, 10, 200f, TimeSpan.Zero,
            new HealthBar(citadelHealBarSprite));
        CitadelManager = new CitadelManager(citadel);
    }

    public static void TestConfig(Vector2 citadelPos)
    {
        var testTower =
            TowerFactory.CreateTower(TowerTypeEnum.Basic, citadelPos + new Vector2(-50, -50));
        TowerManager.AddTower(testTower);
    }
}