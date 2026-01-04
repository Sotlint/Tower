#nullable enable
using GameKit2D.Core;
using GameKit2D.Core.BaseSystems;
using GameKit2D.Core.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Managers;
using Tower.Updaters;

// ReSharper disable PossibleLossOfFraction

namespace Tower;

/// <summary>
/// Главный класс игры. Наследуется от MonoGame.Game и управляет жизненным циклом игры.
/// Отвечает за инициализацию, обновление и отрисовку всех компонентов игры.
/// </summary>
public class GameTower : Game
{
    /// <summary>Менеджер графического устройства для управления настройками экрана</summary>
    private GraphicsDeviceManager _graphics;
    
    /// <summary>SpriteBatch для отрисовки всех 2D элементов (спрайты, текст)</summary>
    private SpriteBatch _spriteBatch;
    
    private GameEngine _engine;

    /// <summary>
    /// Конструктор игры. Инициализирует базовые настройки MonoGame.
    /// </summary>
    public GameTower()
    {
        Content.RootDirectory = "Content"; // Папка с ресурсами (текстуры, шрифты)
        IsMouseVisible = true; // Показываем курсор мыши
        _graphics = new GraphicsDeviceManager(this); // Создаем менеджер графики
        
        // Настройка полноэкранного режима
        _graphics.IsFullScreen = true; // Включаем полноэкранный режим
        _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width; // Ширина экрана
        _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height; // Высота экрана
        _graphics.ApplyChanges(); // Применяем изменения
    }

    /// <summary>
    /// Инициализация игры. Вызывается один раз при запуске приложения.
    /// Создает SpriteBatch, загружает спрайты и инициализирует GameManager.
    /// </summary>
    protected override void Initialize()
    {
        // Создаем SpriteBatch для отрисовки
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        // Загружаем все спрайты и шрифты
        SpriteManager.LoadSprites(Content, GraphicsDevice);
        
        _engine = new GameEngine(
            graphicsDevice: GraphicsDevice,
            spriteBatch: _spriteBatch
        );

        // Регистрация систем
        _engine.Systems.Register(new DebugHelper(GraphicsDevice)
        {
            IsEnabled = true,
            PositionMarkerSize = 1,
            CollisionColor = Color.SpringGreen,
            LineThickness = 1
        });
        _engine.Systems.Register(new BaseUpdateSystem());
        _engine.Systems.Register(new BaseRenderSystem());
        _engine.Systems.Register(new BaseCollisionSystem());
        _engine.Systems.Register(new BaseSpawnSystem());
        _engine.Systems.Register(new BasePerformanceMonitorSystem());
        base.Initialize();
    }

    /// <summary>
    /// Загрузка контента. Вызывается после Initialize().
    /// В текущей реализации не используется, т.к. загрузка происходит в Initialize().
    /// </summary>
    protected override void LoadContent()
    {
    }

    /// <summary>
    /// Обновление игры. Вызывается каждый кадр (обычно 60 раз в секунду).
    /// Делегирует обновление логики в MainUpdateLogic, который обрабатывает
    /// различные состояния игры (Menu, Playing, Planning, Paused, GameOver).
    /// </summary>
    /// <param name="gameTime">Время игры для синхронизации обновлений</param>
    protected override void Update(GameTime gameTime)
    {
        // Обновляем состояние ввода
        InputHelper.Update();
        
        // Обновляем логику игры (ввод, состояние, объекты)
        UpdateLogic.Update(_engine, _spriteBatch, GraphicsDevice, gameTime);
        base.Update(gameTime);
    }

    /// <summary>
    /// Отрисовка игры. Вызывается каждый кадр после Update().
    /// Делегирует отрисовку в MainRenderer, который отрисовывает элементы
    /// в зависимости от текущего состояния игры.
    /// </summary>
    /// <param name="gameTime">Время игры для синхронизации анимаций</param>
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        UpdateRender.Draw(_engine, _spriteBatch, GraphicsDevice, gameTime);
        base.Draw(gameTime);
    }
}