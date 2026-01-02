using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core;
using Tower.Core.Abstractions.Enums;
using Tower.Core.Common;

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
    
    /// <summary>Менеджер точек спавна врагов (управление точками появления врагов)</summary>
    public static SpawnPointManager SpawnPointManager { get; private set; }
    
    /// <summary>Текущий счет игрока (начисляется за убийство врагов)</summary>
    private static int Score { get; set; }
    
    /// <summary>Ссылка на главный класс игры для закрытия приложения</summary>
    private static Game GameInstance { get; set; }
    
    /// <summary>Графическое устройство для получения размеров экрана</summary>
    private static GraphicsDevice GraphicsDeviceInstance { get; set; }

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
    /// <param name="game">Ссылка на главный класс игры (для закрытия приложения)</param>
    public static void Init(GraphicsDevice graphicsDevice, Game game = null)
    {
        // Сохраняем ссылку на главный класс игры для закрытия приложения
        GameInstance = game;
        
        // Сохраняем ссылку на графическое устройство для использования в других методах
        GraphicsDeviceInstance = graphicsDevice;
        
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
        SpawnPointManager = new SpawnPointManager(); // Менеджер точек спавна врагов
        Player = new Player(100); // Начальные деньги: 100 монет
        
        // Инициализация точек спавна по краям экрана
        SpawnPointManager.Initialize(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
        
        // Создание спрайта для полосы здоровья цитадели
        var citadelHealBarSprite = new Texture2D(graphicsDevice, 1, 1);
        citadelHealBarSprite.SetData(new[] { Color.White });
        
        // Позиция цитадели в центре экрана
        var citadelPosition = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);
        
        // Создание цитадели: здоровье 1000, без атаки
        var citadel = new Citadel(Guid.NewGuid(), citadelPosition, 100, 0, 0, TimeSpan.Zero,
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
        
        // Обновляем точки спавна, если доступно графическое устройство
        if (GraphicsDeviceInstance != null && SpawnPointManager != null)
        {
            SpawnPointManager.Initialize(GraphicsDeviceInstance.Viewport.Width, GraphicsDeviceInstance.Viewport.Height);
        }
        
        // Восстанавливаем здоровье цитадели до максимума
        CitadelManager.RestoreHealth();
        
        // Сбрасываем состояние планирования (очищаем временные данные и планируем первую волну)
        LogicUpdaters.PlannedUpdateLogic.Reset();
        
        // Планируем первую волну (если еще не запланирована в Reset)
        var firstWaveEnemyCount = DifficultyManager.GetEnemyCount();
        SpawnPointManager?.PlanNextWave(firstWaveEnemyCount);
        
        // Начинаем с фазы планирования, чтобы игрок мог расставить башни перед первой волной
        GameStateManager.ChangeState(GameStateEnum.Planning);
    }

    /// <summary>
    /// Закрытие игры. Вызывается при нажатии кнопки "Выход" в меню.
    /// Корректно завершает работу приложения.
    /// </summary>
    public static void ExitGame()
    {
        // Закрываем игру через метод Exit() главного класса Game
        GameInstance?.Exit();
    }
}