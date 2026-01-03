using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.LogicUpdaters;
using Tower.Managers;
using Tower.Renderers;

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
        
        // Инициализируем все менеджеры и системы игры
        // Передаем ссылку на этот экземпляр Game для возможности закрытия игры
        GameManager.Init(_graphics.GraphicsDevice, _spriteBatch, this);
        // Игра начинается с главного меню
        
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
        // Обновляем логику игры (ввод, состояние, объекты)
        MainUpdateLogic.Update(gameTime);
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
        // Отрисовываем все элементы игры
        MainRenderer.Render(_spriteBatch, _graphics, gameTime);
        base.Draw(gameTime);
    }
}