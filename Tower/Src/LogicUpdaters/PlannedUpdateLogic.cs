using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Enums;
using Tower.Factories;
using Tower.Managers;

using Microsoft.Xna.Framework.Input;

namespace Tower.LogicUpdaters;

public static class PlannedUpdateLogic
{
    private static bool _waveStarted = false;
    private static TowerTypeEnum? _selectedTowerType = null;
    private static KeyboardState _previousKeyboardState;
    private static ITower _draggedTower = null; // Временная башня для перетаскивания
    private static bool _isDragging = false;

    public static void Update(GameTime gameTime)
    {
        var currentKeyboardState = Keyboard.GetState();
        var mousePos = GameManager.InputManager.MousePosition;
        
        // Обновляем меню выбора башен
        GameManager.UIManager.TowerSelectionMenu.Update(mousePos);
        
        // Обработка начала перетаскивания
        if (GameManager.InputManager.LeftClick)
        {
            var towerType = GameManager.UIManager.TowerSelectionMenu.GetTowerTypeAt(mousePos);
            
            if (towerType != null)
            {
                // Клик по кнопке выбора башни - начинаем перетаскивание
                var cost = TowerFactory.GetTowerCost(towerType.Value);
                if (GameManager.Player.Money >= cost)
                {
                    _selectedTowerType = towerType;
                    _draggedTower = TowerFactory.CreateTower(towerType.Value, mousePos);
                    _isDragging = true;
                }
            }
        }
        
        // Обновление позиции перетаскиваемой башни
        if (_isDragging && _draggedTower != null)
        {
            _draggedTower.SetPosition(mousePos);
            _draggedTower.UpdateBounds();
        }
        
        // Обработка отпускания кнопки мыши (размещение башни)
        if (GameManager.InputManager.LeftButtonReleased && 
            _isDragging && _draggedTower != null)
        {
            if (CanPlaceTower(_draggedTower, mousePos))
            {
                var cost = TowerFactory.GetTowerCost(_selectedTowerType.Value);
                if (GameManager.Player.SpendMoney(cost))
                {
                    GameManager.TowerManager.AddTower(_draggedTower);
                    _draggedTower = null;
                }
            }
            
            _isDragging = false;
            _selectedTowerType = null;
            _draggedTower = null;
        }
        
        // Обновление кнопки "Начать волну"
        if (GameManager.UIManager.StartWaveButton != null)
        {
            GameManager.UIManager.StartWaveButton.Update(mousePos);
            if (GameManager.InputManager.LeftClick && 
                GameManager.UIManager.StartWaveButton.HandleClick(mousePos))
            {
                if (!_waveStarted)
                {
                    StartWave();
                }
            }
        }
        
        // Обработка начала волны по нажатию Space
        if (currentKeyboardState.IsKeyDown(Keys.Space) && 
            _previousKeyboardState.IsKeyUp(Keys.Space) && 
            !_waveStarted)
        {
            StartWave();
        }
        
        _previousKeyboardState = currentKeyboardState;
    }
    
    /// <summary>
    /// Проверка возможности размещения башни (коллизии и валидность позиции)
    /// </summary>
    private static bool CanPlaceTower(ITower tower, Vector2 position)
    {
        // Проверка коллизий с другими башнями
        var existingTowers = GameManager.TowerManager.GetTowers();
        foreach (var existingTower in existingTowers)
        {
            if (tower.Bounds.Intersects(existingTower.Bounds))
            {
                return false;
            }
        }
        
        // Проверка коллизий с цитаделью
        var citadel = GameManager.CitadelManager.GetCitadel();
        if (tower.Bounds.Intersects(citadel.Bounds))
        {
            return false;
        }
        
        // Проверка, что башня не в области меню
        var menuBounds = GameManager.UIManager.TowerSelectionMenu.GetMenuBounds();
        if (menuBounds != null && tower.Bounds.Intersects(menuBounds.Value))
        {
            return false;
        }
        
        return true;
    }
    
    /// <summary>
    /// Отрисовка перетаскиваемой башни
    /// </summary>
    public static void DrawDraggedTower(SpriteBatch spriteBatch, GameTime gameTime)
    {
        if (_isDragging && _draggedTower != null)
        {
            var canPlace = CanPlaceTower(_draggedTower, _draggedTower.Position);
            var color = canPlace ? Color.White : Color.Red * 0.7f; // Красный если нельзя разместить
            
            spriteBatch.Begin();
            spriteBatch.Draw(
                _draggedTower.Sprite,
                _draggedTower.Position,
                null,
                color,
                0f,
                new Vector2(_draggedTower.Sprite.Width / 2f, _draggedTower.Sprite.Height / 2f),
                _draggedTower.SpriteScale,
                SpriteEffects.None,
                0f
            );
            spriteBatch.End();
        }
    }
    
    /// <summary>
    /// Начать волну врагов
    /// </summary>
    public static void StartWave()
    {
        _waveStarted = true;
        GameManager.EnemyManager.SpawnEnemy(
            GameManager.DifficultyManager.GetEnemyCount());
        GameManager.GameStateManager.ChangeState(GameStateEnum.Playing);
    }
    
    /// <summary>
    /// Сброс состояния планирования (вызывается при переходе в Planning)
    /// </summary>
    public static void Reset()
    {
        _waveStarted = false;
        _selectedTowerType = null;
        _isDragging = false;
        _draggedTower = null;
    }
}