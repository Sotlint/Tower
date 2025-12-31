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
    public static InputManager InputManager { get; private set; }
    public static UIManager UIManager { get; private set; }
    private static int Score { get; set; }

    public static void UpdateScore(int points)
    {
        Score += points;
    }

    public static int GetScore() => Score;

    public static void Init(GraphicsDevice graphicsDevice)
    {
        Score = 0;
        TowerManager = new TowerManager();
        GameStateManager = new GameStateManager(); // Начинаем с Menu
        EnemyManager = new EnemyManager();
        InputManager = new InputManager();
        DifficultyManager = new DifficultyManager();
        ProjectileManager = new ProjectileManager();
        UIManager = new UIManager();
        Player = new Player(100);
        
        var citadelHealBarSprite = new Texture2D(graphicsDevice, 1, 1);
        citadelHealBarSprite.SetData(new[] { Color.White });
        var citadelPosition = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);
        var citadel = new Citadel(Guid.NewGuid(), citadelPosition, 1000, 0, 0, TimeSpan.Zero,
            new HealthBar(citadelHealBarSprite));
        CitadelManager = new CitadelManager(citadel);
    }
    
    /// <summary>
    /// Инициализация новой игры (вызывается при нажатии "Начать игру")
    /// </summary>
    public static void StartNewGame()
    {
        Score = 0;
        TowerManager = new TowerManager();
        DifficultyManager = new DifficultyManager();
        EnemyManager = new EnemyManager();
        ProjectileManager = new ProjectileManager();
        Player = new Player(100);
        
        // Восстанавливаем здоровье цитадели
        CitadelManager.RestoreHealth();
        
        // Сбрасываем состояние планирования
        LogicUpdaters.PlannedUpdateLogic.Reset();
        
        // Начинаем с фазы планирования, чтобы игрок мог расставить башни
        GameStateManager.ChangeState(GameStateEnum.Planning);
    }

    public static void TestConfig(Vector2 citadelPos)
    {
        var testTower =
            TowerFactory.CreateTower(TowerTypeEnum.Basic, citadelPos + new Vector2(-50, -50));
        TowerManager.AddTower(testTower);
    }
}