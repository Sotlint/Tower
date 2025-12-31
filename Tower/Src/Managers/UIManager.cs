using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions.Enums;
using Tower.LogicUpdaters;
using Tower.UI;

namespace Tower.Managers;

public class UIManager
{
    public TowerSelectionMenu TowerSelectionMenu { get; private set; }
    public MainMenu MainMenu { get; private set; }
    public PauseMenu PauseMenu { get; private set; }
    public GameOverMenu GameOverMenu { get; private set; }
    public UIButton StartWaveButton { get; private set; }
    private bool _startWaveButtonInitialized = false;

    public UIManager()
    {
        TowerSelectionMenu = new TowerSelectionMenu();
        MainMenu = new MainMenu();
        PauseMenu = new PauseMenu();
        GameOverMenu = new GameOverMenu();
    }
    
    private void InitializeStartWaveButton(GraphicsDevice graphicsDevice)
    {
        if (_startWaveButtonInitialized) return;
        
        var screenWidth = graphicsDevice.Viewport.Width;
        var screenHeight = graphicsDevice.Viewport.Height;
        var buttonWidth = 150;
        var buttonHeight = 50;
        var buttonX = screenWidth - buttonWidth - 20; // Справа с отступом
        var buttonY = screenHeight - 100; // Снизу, выше меню башен
        
        StartWaveButton = new UIButton(new Rectangle(buttonX, buttonY, buttonWidth, buttonHeight), "Начать волну")
        {
            OnClick = () => PlannedUpdateLogic.StartWave()
        };
        
        _startWaveButtonInitialized = true;
    }
    
    public void DrawStartWaveButton(SpriteBatch spriteBatch)
    {
        InitializeStartWaveButton(spriteBatch.GraphicsDevice);
        if (StartWaveButton != null)
        {
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