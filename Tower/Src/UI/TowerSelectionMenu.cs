using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.UI;

/// <summary>
/// Меню выбора башен для размещения. Отображается внизу экрана по центру
/// в горизонтальном расположении с окантовкой и полупрозрачным фоном.
/// </summary>
public class TowerSelectionMenu
{
    /// <summary>Список кнопок выбора башен</summary>
    private readonly List<UIButton<TowerTypeEnum>> _towerButtons = new();
    
    /// <summary>Границы области меню</summary>
    private Rectangle _menuBounds;
    
    /// <summary>Флаг инициализации меню (ленивая инициализация при первом Draw)</summary>
    private bool _isInitialized = false;

    /// <summary>Текстура фона меню (не используется, фон создается программно)</summary>
    public Texture2D BackgroundTexture { get; set; }
    
    /// <summary>Текстура рамки меню (не используется, рамка создается программно)</summary>
    public Texture2D FrameTexture { get; set; }

    /// <summary>
    /// Конструктор меню выбора башен. Инициализация отложена до первого вызова Draw,
    /// когда будет доступен GraphicsDevice для вычисления размеров экрана.
    /// </summary>
    public TowerSelectionMenu()
    {
        // Инициализация будет выполнена при первом Draw, когда GraphicsDevice доступен
    }

    /// <summary>
    /// Инициализация меню. Вычисляет позиции и размеры элементов меню
    /// на основе размеров экрана и создает кнопки выбора башен.
    /// </summary>
    /// <param name="graphicsDevice">Графическое устройство для получения размеров экрана</param>
    private void Initialize(GraphicsDevice graphicsDevice)
    {
        // Инициализация выполняется только один раз
        if (_isInitialized) return;
        
        // Параметры размеров элементов меню
        var buttonSize = 64; // Размер кнопки башни (64x64 пикселей)
        var padding = 10; // Отступ между кнопками и от краев
        // Все доступные типы башен
        var types = new[] 
        { 
            TowerTypeEnum.Stone,
            TowerTypeEnum.Fire,
            TowerTypeEnum.Ice,
            TowerTypeEnum.Light,
            TowerTypeEnum.Poison
        };
        
        // Получаем размеры экрана
        var screenWidth = graphicsDevice.Viewport.Width;
        var screenHeight = graphicsDevice.Viewport.Height;
        
        // Отступ между кнопками (фиксированный 10 пикселей)
        var spacingBetweenButtons = 10;
        
        // Вычисляем ширину меню: количество кнопок * размер + отступы между кнопками + отступы по краям
        var menuWidth = types.Length * buttonSize + (types.Length - 1) * spacingBetweenButtons + padding * 2;
        var menuHeight = buttonSize + padding * 2; // Высота = размер кнопки + отступы сверху и снизу
        
        // Позиция меню: по центру горизонтально, снизу с отступом 20px
        var menuX = (screenWidth - menuWidth) / 2; // Центрируем по горизонтали
        var menuY = screenHeight - menuHeight - 20; // Снизу с отступом 20px
        
        // Сохраняем границы меню
        _menuBounds = new Rectangle(menuX, menuY, menuWidth, menuHeight);
        
        // Создаем кнопки выбора башен в горизонтальном расположении
        for (var i = 0; i < types.Length; i++)
        {
            var type = types[i];
            // Вычисляем позицию кнопки: начало меню + отступ + индекс * (размер + отступ между кнопками)
            var x = menuX + padding + i * (buttonSize + spacingBetweenButtons);
            var y = menuY + padding; // Все кнопки на одной высоте
            var bounds = new Rectangle(x, y, buttonSize, buttonSize);

            // Получаем спрайт для данного типа башни
            Texture2D iconTexture = type switch
            {
                TowerTypeEnum.Stone => SpriteManager.StoneTowerSprite,
                TowerTypeEnum.Fire => SpriteManager.FireTowerSprite,
                TowerTypeEnum.Ice => SpriteManager.IceTowerSprite,
                TowerTypeEnum.Light => SpriteManager.LightTowerSprite,
                TowerTypeEnum.Poison => SpriteManager.PoisonTowerSprite,
                _ => SpriteManager.OldTowerSprite // Резервный спрайт
            };
            
            // Создаем кнопку с типом башни и иконкой
            var button = new UIButton<TowerTypeEnum>(bounds, type)
            {
                BackgroundTexture = SpriteManager.MenuButtonTexture, // Текстура фона кнопки
                IconTexture = iconTexture, // Иконка башни на кнопке
            };

            _towerButtons.Add(button);
        }
        
        // Меню инициализировано
        _isInitialized = true;
    }

    /// <summary>
    /// Обновление состояния меню. Обновляет hover эффекты на кнопках.
    /// </summary>
    /// <param name="mousePosition">Текущая позиция курсора мыши</param>
    public void Update(Vector2 mousePosition)
    {
        // Обновляем состояние каждой кнопки (hover эффекты)
        foreach (var button in _towerButtons)
        {
            button.Update(mousePosition);
        }
    }

    /// <summary>
    /// Отрисовка меню выбора башен. Отрисовывает фон и кнопки.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    public void Draw(SpriteBatch spriteBatch)
    {
        // Инициализируем меню при первом вызове (когда GraphicsDevice доступен)
        Initialize(spriteBatch.GraphicsDevice);
        
        spriteBatch.Begin();
        
        // Отрисовка фона меню (текстура paper-horiz, растянутая на всю ширину)
        if (SpriteManager.PaperHorizTexture != null)
        {
            spriteBatch.Draw(SpriteManager.PaperHorizTexture, _menuBounds, Color.White);
        }
        else
        {
            // Резервный вариант: полупрозрачный темный фон, если текстура не загружена
            var backgroundColor = new Color(50, 50, 50, 200);
            var backgroundTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            backgroundTexture.SetData(new[] { backgroundColor });
            spriteBatch.Draw(backgroundTexture, _menuBounds, backgroundColor);
        }
        
        // Отрисовка кнопок выбора башен
        foreach (var button in _towerButtons)
        {
            button.Draw(spriteBatch);
        }
        
        spriteBatch.End();
    }

    /// <summary>
    /// Получить тип башни по позиции курсора. Используется для определения,
    /// была ли нажата кнопка выбора башни.
    /// </summary>
    /// <param name="mousePosition">Позиция курсора мыши</param>
    /// <returns>Тип башни, если курсор над кнопкой, иначе null</returns>
    public TowerTypeEnum? GetTowerTypeAt(Vector2 mousePosition)
    {
        // Проверяем каждую кнопку на пересечение с позицией курсора
        foreach (var button in _towerButtons)
        {
            if (button.Bounds.Contains(mousePosition))
                return button.Payload; // Возвращаем тип башни из кнопки
        }

        return null; // Курсор не над кнопками
    }
    
    /// <summary>
    /// Получить границы меню для проверки коллизий
    /// </summary>
    public Rectangle? GetMenuBounds()
    {
        return _isInitialized ? _menuBounds : null;
    }
}