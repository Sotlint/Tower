using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Enums;
using Tower.Factories;
using Tower.Managers;

using Microsoft.Xna.Framework.Input;

namespace Tower.LogicUpdaters;

/// <summary>
/// Логика обновления фазы планирования. Управляет размещением башен,
/// перетаскиванием, проверкой коллизий и началом волн врагов.
/// </summary>
public static class PlannedUpdateLogic
{
    /// <summary>Флаг, указывающий, была ли начата волна врагов</summary>
    private static bool _waveStarted = false;
    
    /// <summary>Выбранный тип башни для размещения</summary>
    private static TowerTypeEnum? _selectedTowerType = null;
    
    /// <summary>Предыдущее состояние клавиатуры для определения нажатий клавиш</summary>
    private static KeyboardState _previousKeyboardState;
    
    /// <summary>Временная башня, которая следует за курсором при перетаскивании</summary>
    private static ITower _draggedTower = null;
    
    /// <summary>Флаг, указывающий, происходит ли в данный момент перетаскивание башни</summary>
    private static bool _isDragging = false;

    /// <summary>
    /// Обновление логики планирования. Обрабатывает перетаскивание башен,
    /// размещение, проверку коллизий и начало волн.
    /// </summary>
    /// <param name="gameTime">Время игры для синхронизации</param>
    public static void Update(GameTime gameTime)
    {
        // Получаем текущее состояние клавиатуры и позицию мыши
        var currentKeyboardState = Keyboard.GetState();
        var mousePos = GameManager.InputManager.MousePosition;
        
        // Обновляем состояние меню выбора башен (hover эффекты на кнопках)
        GameManager.UIManager.TowerSelectionMenu.Update(mousePos);
        
        // Обработка начала перетаскивания башни
        if (GameManager.InputManager.LeftClick)
        {
            // Проверяем, был ли клик по кнопке выбора башни
            var towerType = GameManager.UIManager.TowerSelectionMenu.GetTowerTypeAt(mousePos);
            
            if (towerType != null)
            {
                // Клик по кнопке выбора башни - начинаем перетаскивание
                // Проверяем, достаточно ли денег для покупки башни
                var cost = TowerFactory.GetTowerCost(towerType.Value);
                if (GameManager.Player.Money >= cost)
                {
                    // Сохраняем выбранный тип и создаем временную башню для перетаскивания
                    _selectedTowerType = towerType;
                    _draggedTower = TowerFactory.CreateTower(towerType.Value, mousePos);
                    _isDragging = true;
                }
            }
        }
        
        // Обновление позиции перетаскиваемой башни (следует за курсором)
        if (_isDragging && _draggedTower != null)
        {
            _draggedTower.SetPosition(mousePos);
            _draggedTower.UpdateBounds(); // Обновляем границы для проверки коллизий
        }
        
        // Обработка отпускания кнопки мыши (размещение башни)
        if (GameManager.InputManager.LeftButtonReleased && 
            _isDragging && _draggedTower != null)
        {
            // Проверяем, можно ли разместить башню в текущей позиции
            if (CanPlaceTower(_draggedTower, mousePos))
            {
                // Списываем деньги и добавляем башню на карту
                var cost = TowerFactory.GetTowerCost(_selectedTowerType.Value);
                if (GameManager.Player.SpendMoney(cost))
                {
                    GameManager.TowerManager.AddTower(_draggedTower);
                    _draggedTower = null; // Башня добавлена, временная больше не нужна
                }
            }
            
            // Сбрасываем состояние перетаскивания
            _isDragging = false;
            _selectedTowerType = null;
            _draggedTower = null;
        }
        
        // Обновление кнопки "Начать волну" (hover эффекты)
        if (GameManager.UIManager.StartWaveButton != null)
        {
            GameManager.UIManager.StartWaveButton.Update(mousePos);
            // Обработка клика по кнопке "Начать волну"
            if (GameManager.InputManager.LeftClick && 
                GameManager.UIManager.StartWaveButton.HandleClick(mousePos))
            {
                if (!_waveStarted)
                {
                    StartWave();
                }
            }
        }
        
        // Обработка начала волны по нажатию клавиши Space (альтернативный способ)
        if (currentKeyboardState.IsKeyDown(Keys.Space) && 
            _previousKeyboardState.IsKeyUp(Keys.Space) && 
            !_waveStarted)
        {
            StartWave();
        }
        
        // Сохраняем текущее состояние клавиатуры для следующего кадра
        _previousKeyboardState = currentKeyboardState;
    }
    
    /// <summary>
    /// Проверка возможности размещения башни в указанной позиции.
    /// Проверяет коллизии с другими башнями, цитаделью и областью меню.
    /// </summary>
    /// <param name="tower">Башня для проверки</param>
    /// <param name="position">Позиция для размещения (не используется, т.к. позиция уже в tower)</param>
    /// <returns>true, если башню можно разместить, false - если есть коллизии</returns>
    private static bool CanPlaceTower(ITower tower, Vector2 position)
    {
        // Проверка коллизий с другими башнями на карте
        var existingTowers = GameManager.TowerManager.GetTowers();
        foreach (var existingTower in existingTowers)
        {
            if (tower.Bounds.Intersects(existingTower.Bounds))
            {
                return false; // Есть пересечение с другой башней
            }
        }
        
        // Проверка коллизий с цитаделью (башню нельзя размещать на цитадели)
        var citadel = GameManager.CitadelManager.GetCitadel();
        if (tower.Bounds.Intersects(citadel.Bounds))
        {
            return false; // Есть пересечение с цитаделью
        }
        
        // Проверка, что башня не размещается в области меню выбора башен
        var menuBounds = GameManager.UIManager.TowerSelectionMenu.GetMenuBounds();
        if (menuBounds != null && tower.Bounds.Intersects(menuBounds.Value))
        {
            return false; // Башня попадает в область меню
        }
        
        // Все проверки пройдены - башню можно разместить
        return true;
    }
    
    /// <summary>
    /// Отрисовка перетаскиваемой башни. Башня следует за курсором мыши
    /// и меняет цвет в зависимости от возможности размещения.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="gameTime">Время игры (не используется, но требуется для совместимости)</param>
    public static void DrawDraggedTower(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Отрисовываем только если происходит перетаскивание
        if (_isDragging && _draggedTower != null)
        {
            // Проверяем, можно ли разместить башню в текущей позиции
            var canPlace = CanPlaceTower(_draggedTower, _draggedTower.Position);
            // Белый цвет - можно разместить, красный - нельзя
            var color = canPlace ? Color.White : Color.Red * 0.7f;
            
            spriteBatch.Begin();
            // Отрисовываем башню с центрированием по точке привязки
            spriteBatch.Draw(
                _draggedTower.Sprite, // Текстура башни
                _draggedTower.Position, // Позиция (следует за курсором)
                null, // Область текстуры (null = вся текстура)
                color, // Цвет с учетом возможности размещения
                0f, // Угол поворота
                new Vector2(_draggedTower.Sprite.Width / 2f, _draggedTower.Sprite.Height / 2f), // Точка привязки (центр)
                _draggedTower.SpriteScale, // Масштаб
                SpriteEffects.None, // Эффекты отображения
                0f // Глубина слоя
            );
            spriteBatch.End();
        }
    }
    
    /// <summary>
    /// Начать волну врагов. Спавнит врагов и переводит игру в состояние Playing.
    /// </summary>
    public static void StartWave()
    {
        _waveStarted = true;
        // Спавним врагов в количестве, соответствующем текущему уровню сложности
        GameManager.EnemyManager.SpawnEnemy(
            GameManager.DifficultyManager.GetEnemyCount());
        // Переводим игру в состояние активной волны
        GameManager.GameStateManager.ChangeState(GameStateEnum.Playing);
    }
    
    /// <summary>
    /// Сброс состояния планирования. Вызывается при переходе в состояние Planning
    /// для очистки временных данных и подготовки к новому циклу планирования.
    /// </summary>
    public static void Reset()
    {
        _waveStarted = false; // Сбрасываем флаг начала волны
        _selectedTowerType = null; // Сбрасываем выбранный тип башни
        _isDragging = false; // Сбрасываем флаг перетаскивания
        _draggedTower = null; // Удаляем временную башню
    }
}