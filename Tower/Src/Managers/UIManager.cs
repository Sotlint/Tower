using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions.Enums;
using Tower.LogicUpdaters;
using Tower.UI;

namespace Tower.Managers;

/// <summary>
/// Менеджер пользовательского интерфейса. Управляет всеми UI элементами игры:
/// меню, кнопками, панелями и т.д.
/// </summary>
public class UIManager
{
    /// <summary>Меню выбора башен для размещения</summary>
    public TowerSelectionMenu TowerSelectionMenu { get; private set; }
    
    /// <summary>Главное меню игры</summary>
    public MainMenu MainMenu { get; private set; }
    
    /// <summary>Меню паузы</summary>
    public PauseMenu PauseMenu { get; private set; }
    
    /// <summary>Меню окончания игры</summary>
    public GameOverMenu GameOverMenu { get; private set; }
    
    /// <summary>Кнопка начала волны врагов</summary>
    public UIButton StartWaveButton { get; private set; }
    
    /// <summary>HUD игры (отображение счета, денег и другой информации)</summary>
    public GameHUD GameHUD { get; private set; }
    
    /// <summary>Флаг инициализации кнопки начала волны (ленивая инициализация)</summary>
    private bool _startWaveButtonInitialized = false;

    /// <summary>
    /// Конструктор менеджера UI. Инициализирует все меню игры.
    /// </summary>
    public UIManager()
    {
        TowerSelectionMenu = new TowerSelectionMenu();
        MainMenu = new MainMenu();
        PauseMenu = new PauseMenu();
        GameOverMenu = new GameOverMenu();
        GameHUD = new GameHUD();
    }
    
    /// <summary>
    /// Инициализация кнопки "Начать волну". Вычисляет позицию кнопки
    /// на основе размеров экрана и создает кнопку.
    /// </summary>
    /// <param name="graphicsDevice">Графическое устройство для получения размеров экрана</param>
    private void InitializeStartWaveButton(GraphicsDevice graphicsDevice)
    {
        // Инициализация выполняется только один раз
        if (_startWaveButtonInitialized) return;
        
        // Получаем размеры экрана
        var screenWidth = graphicsDevice.Viewport.Width;
        var screenHeight = graphicsDevice.Viewport.Height;
        
        // Параметры кнопки
        var buttonWidth = 150; // Ширина кнопки
        var buttonHeight = 50; // Высота кнопки
        var buttonX = screenWidth - buttonWidth - 20; // Справа с отступом 20px
        var buttonY = screenHeight - 100; // Снизу, выше меню башен (отступ 100px)
        
        // Создаем кнопку с текстом и обработчиком клика
        StartWaveButton = new UIButton(new Rectangle(buttonX, buttonY, buttonWidth, buttonHeight), "Начать волну")
        {
            OnClick = () => PlannedUpdateLogic.StartWave() // При клике начинаем волну
        };
        
        _startWaveButtonInitialized = true;
    }
    
    /// <summary>
    /// Отрисовка кнопки "Начать волну". Инициализирует кнопку при первом вызове
    /// и отрисовывает её с правильным Begin/End для SpriteBatch.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    public void DrawStartWaveButton(SpriteBatch spriteBatch)
    {
        // Инициализируем кнопку при первом вызове (когда GraphicsDevice доступен)
        InitializeStartWaveButton(spriteBatch.GraphicsDevice);
        
        if (StartWaveButton != null)
        {
            // Оборачиваем отрисовку в Begin/End, т.к. UIButton.Draw() не вызывает их самостоятельно
            spriteBatch.Begin();
            StartWaveButton.Draw(spriteBatch);
            spriteBatch.End();
        }
    }

    /// <summary>
    /// Получить текущее активное меню в зависимости от состояния игры
    /// </summary>
    public BaseMenu GetActiveMenu()
    {
        var state = GameManager.GameStateManager.CurrentState;
        return state switch
        {
            GameStateEnum.Menu => MainMenu,
            GameStateEnum.Paused => PauseMenu,
            GameStateEnum.GameOver => GameOverMenu,
            _ => null
        };
    }
}