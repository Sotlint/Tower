using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core;
using Tower.Core.Abstractions.Enums;
using Tower.Factories;

namespace Tower.Managers;

/// <summary>
/// Главный менеджер игры. Управление основными системами и состоянием игры,
/// включая игрока, башни, врагов, цитадель, сложность и спрайты.
/// Ответственность за инициализацию и координацию основных менеджеров и отслеживание счета игрока.
/// </summary>
public static class GameManager
{
    /// <summary>Игрок (управляет экономикой - деньгами)</summary>
    public static Player Player { get; private set; }
    
    /// <summary>Менеджер состояний игры (Menu, Playing, Planning, Paused, GameOver)</summary>
    public static GameStateManager GameStateManager { get; private set; }
    
    /// <summary>Менеджер башен (создание, обновление, отрисовка)</summary>
    public static TowerManager TowerManager { get; private set; }
    
    /// <summary>Менеджер цитадели (главная цель врагов)</summary>
    public static CitadelManager CitadelManager { get; private set; }
    
    /// <summary>Менеджер врагов (спавн, обновление, отрисовка)</summary>
    public static EnemyManager EnemyManager { get; private set; }
    
    /// <summary>Менеджер сложности (управление уровнем сложности и количеством врагов)</summary>
    public static DifficultyManager DifficultyManager { get; private set; }
    
    /// <summary>Менеджер снарядов (создание, обновление, отрисовка)</summary>
    public static ProjectileManager ProjectileManager { get; private set; }
    
    /// <summary>Менеджер ввода (клавиатура, мышь)</summary>
    public static InputManager InputManager { get; private set; }
    
    /// <summary>Менеджер UI (меню, кнопки, интерфейс)</summary>
    public static UIManager UIManager { get; private set; }
    
    /// <summary>Текущий счет игрока (начисляется за убийство врагов)</summary>
    private static int Score { get; set; }

    /// <summary>
    /// Обновление счета игрока. Добавляет очки к текущему счету.
    /// </summary>
    /// <param name="points">Количество очков для добавления</param>
    public static void UpdateScore(int points)
    {
        Score += points;
    }

    /// <summary>
    /// Получить текущий счет игрока.
    /// </summary>
    /// <returns>Текущий счет</returns>
    public static int GetScore() => Score;

    /// <summary>
    /// Инициализация игры. Вызывается при запуске приложения.
    /// Создает все менеджеры, инициализирует цитадель и устанавливает начальное состояние.
    /// </summary>
    /// <param name="graphicsDevice">Графическое устройство для создания текстур и получения размеров экрана</param>
    public static void Init(GraphicsDevice graphicsDevice)
    {
        // Инициализация счета
        Score = 0;
        
        // Создание всех менеджеров
        TowerManager = new TowerManager();
        GameStateManager = new GameStateManager(); // Начинаем с Menu
        EnemyManager = new EnemyManager();
        InputManager = new InputManager();
        DifficultyManager = new DifficultyManager();
        ProjectileManager = new ProjectileManager();
        UIManager = new UIManager();
        Player = new Player(100); // Начальные деньги: 100 монет
        
        // Создание спрайта для полосы здоровья цитадели
        var citadelHealBarSprite = new Texture2D(graphicsDevice, 1, 1);
        citadelHealBarSprite.SetData(new[] { Color.White });
        
        // Позиция цитадели в центре экрана
        var citadelPosition = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);
        
        // Создание цитадели: здоровье 1000, без атаки
        var citadel = new Citadel(Guid.NewGuid(), citadelPosition, 1000, 0, 0, TimeSpan.Zero,
            new HealthBar(citadelHealBarSprite));
        CitadelManager = new CitadelManager(citadel);
    }
    
    /// <summary>
    /// Инициализация новой игры. Вызывается при нажатии "Начать игру" в главном меню.
    /// Сбрасывает все игровые данные, восстанавливает здоровье цитадели и переводит игру в фазу планирования.
    /// </summary>
    public static void StartNewGame()
    {
        // Сброс счета и создание новых менеджеров
        Score = 0;
        TowerManager = new TowerManager(); // Очищаем все башни
        DifficultyManager = new DifficultyManager(); // Сбрасываем сложность на уровень 1
        EnemyManager = new EnemyManager(); // Очищаем всех врагов
        ProjectileManager = new ProjectileManager(); // Очищаем все снаряды
        Player = new Player(100); // Даем игроку 100 монет на начало
        
        // Восстанавливаем здоровье цитадели до максимума
        CitadelManager.RestoreHealth();
        
        // Сбрасываем состояние планирования (очищаем временные данные)
        LogicUpdaters.PlannedUpdateLogic.Reset();
        
        // Начинаем с фазы планирования, чтобы игрок мог расставить башни перед первой волной
        GameStateManager.ChangeState(GameStateEnum.Planning);
    }

    /// <summary>
    /// Тестовая конфигурация. Создает тестовую башню для отладки.
    /// TODO: Удалить или сделать опциональной после завершения разработки.
    /// </summary>
    /// <param name="citadelPos">Позиция цитадели (для размещения тестовой башни рядом)</param>
    public static void TestConfig(Vector2 citadelPos)
    {
        // Создаем тестовую башню рядом с цитаделью
        var testTower =
            TowerFactory.CreateTower(TowerTypeEnum.Basic, citadelPos + new Vector2(-50, -50));
        TowerManager.AddTower(testTower);
    }
}