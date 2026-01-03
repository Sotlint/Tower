using System;
using System.Collections.Generic;
using System.Linq;
using GameKit2D.Abstractions.Interfaces.GameObjects;
using GameKit2D.Core;
using GameKit2D.Core.BaseSystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Enums;
using Tower.Core.Common;
using Tower.Systems;

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
    
    /// <summary>Менеджер цитадели (главная цель врагов)</summary>
    public static CitadelManager CitadelManager { get; private set; }
    
    /// <summary>Менеджер сложности (управление уровнем сложности и количеством врагов)</summary>
    public static DifficultyManager DifficultyManager { get; private set; }
    
    /// <summary>Менеджер ввода (клавиатура, мышь)</summary>
    public static InputManager InputManager { get; private set; }
    
    /// <summary>Менеджер UI (меню, кнопки, интерфейс)</summary>
    public static UIManager UIManager { get; private set; }
    
    /// <summary>Менеджер точек спавна врагов (управление точками появления врагов)</summary>
    public static SpawnPointManager SpawnPointManager { get; private set; }
    
    /// <summary>Игровой движок GameKit2D для управления объектами и системами</summary>
    public static GameEngine GameEngine { get; private set; }
    
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
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="game">Ссылка на главный класс игры (для закрытия приложения)</param>
    public static void Init(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Game game = null)
    {
        // Сохраняем ссылку на главный класс игры для закрытия приложения
        GameInstance = game;
        
        // Сохраняем ссылку на графическое устройство для использования в других методах
        GraphicsDeviceInstance = graphicsDevice;
        
        // Инициализация счета
        Score = 0;
        
        // Создание всех менеджеров
        GameStateManager = new GameStateManager(); // Начинаем с Menu
        InputManager = new InputManager();
        DifficultyManager = new DifficultyManager();
        UIManager = new UIManager();
        SpawnPointManager = new SpawnPointManager(); // Менеджер точек спавна врагов
        Player = new Player(100); // Начальные деньги: 100 монет
        
        // Инициализация точек спавна по краям экрана
        SpawnPointManager.Initialize(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
        
        // Создание GameEngine
        GameEngine = new GameEngine(graphicsDevice, spriteBatch);
        
        // Регистрация базовых систем
        GameEngine.Systems.Register(new BaseUpdateSystem());
        GameEngine.Systems.Register(new BaseRenderSystem());
        
        // Регистрация кастомных систем
        var enemyDeathSystem = new EnemyDeathSystem(GameEngine);
        GameEngine.Systems.Register(enemyDeathSystem);
        
        var projectileCleanupSystem = new ProjectileCleanupSystem(GameEngine);
        GameEngine.Systems.Register(projectileCleanupSystem);
        
        // Создание спрайта для полосы здоровья цитадели
        var citadelHealBarSprite = new Texture2D(graphicsDevice, 1, 1);
        citadelHealBarSprite.SetData(new[] { Color.White });
        
        // Позиция цитадели в центре экрана
        var citadelPosition = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);
        
        // Создание цитадели: здоровье 100, без атаки
        var citadel = new Citadel(Guid.NewGuid(), citadelPosition, 100m, 0m, 0, TimeSpan.Zero,
            new HealthBar(citadelHealBarSprite));
        CitadelManager = new CitadelManager(citadel);
        
        // Добавляем цитадель в GameEngine с правильным слоем
        AddObject(citadel);
    }
    
    /// <summary>
    /// Инициализация новой игры. Вызывается при нажатии "Начать игру" в главном меню.
    /// Сбрасывает все игровые данные, восстанавливает здоровье цитадели и переводит игру в фазу планирования.
    /// </summary>
    public static void StartNewGame()
    {
        // Сброс счета
        Score = 0;
        DifficultyManager = new DifficultyManager(); // Сбрасываем сложность на уровень 1
        Player = new Player(100); // Даем игроку 100 монет на начало
        
        // Очищаем GameEngine (удаляем все объекты, кроме цитадели)
        var citadel = CitadelManager.GetCitadel();
        var allObjects = GameEngine.Objects.GetAll().ToList();
        foreach (var obj in allObjects)
        {
            if (obj.Id != citadel.Id)
            {
                GameEngine.Remove(obj.Id);
            }
        }
        
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
    
    /// <summary>
    /// Получить все объекты указанного типа из GameEngine.
    /// </summary>
    /// <typeparam name="T">Тип объектов</typeparam>
    /// <returns>Коллекция объектов указанного типа</returns>
    public static IEnumerable<T> GetObjectsOfType<T>() where T : class, IHaveIdentity
    {
        return GameEngine?.GetObjectsOfType<T>() ?? Enumerable.Empty<T>();
    }
    
    /// <summary>
    /// Получить все объекты, реализующие указанный интерфейс, из GameEngine.
    /// </summary>
    /// <typeparam name="T">Тип интерфейса</typeparam>
    /// <returns>Коллекция объектов, реализующих интерфейс</returns>
    public static IEnumerable<T> GetObjectsWithInterface<T>() where T : class
    {
        return GameEngine?.GetObjectsWithInterface<T>() ?? Enumerable.Empty<T>();
    }
    
    /// <summary>
    /// Удалить объект из GameEngine.
    /// </summary>
    /// <param name="obj">Объект для удаления</param>
    public static void RemoveObject(IHaveIdentity obj)
    {
        if (obj != null && GameEngine != null)
        {
            GameEngine.Remove(obj.Id);
        }
    }
    
    /// <summary>
    /// Добавить объект в GameEngine с правильным слоем отрисовки.
    /// </summary>
    /// <param name="obj">Объект для добавления</param>
    public static void AddObject(IHaveIdentity obj)
    {
        if (obj == null || GameEngine == null)
            return;
            
        // Добавляем объект в GameEngine (автоматически регистрируется в системах)
        GameEngine.Add(obj);
        
        // Определяем слой отрисовки на основе типа объекта
        var renderSystem = GameEngine.Systems.Get<BaseRenderSystem>();
        if (renderSystem != null && obj is IHaveDrawLogic drawable)
        {
            int layer = 0; // По умолчанию слой 0
            
            // Определяем слой на основе типа объекта
            if (obj is ITower || obj is IBuilding)
            {
                layer = 1; // Башни и цитадель - слой 1
            }
            else if (obj is IProjectile)
            {
                layer = 2; // Снаряды - слой 2
            }
            else if (obj is IEnemy)
            {
                layer = 3; // Враги - слой 3
            }
            
            // Обновляем слой объекта в системе отрисовки
            // Сначала удаляем из системы (если уже добавлен автоматически)
            renderSystem.RemoveObject(drawable);
            // Затем добавляем с правильным слоем
            renderSystem.AddObject(drawable, layer);
        }
    }
}