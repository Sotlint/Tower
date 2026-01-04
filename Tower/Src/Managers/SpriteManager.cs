using GameKit2D.Core.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tower.Managers;

/// <summary>
/// Менеджер спрайтов. Отвечает за загрузку и хранение всех текстур и шрифтов, используемых в игре.
/// Предоставляет свойства для доступа к основным спрайтам: врагов, цитадели, башни, стены,
/// а также программно созданным текстурам (полосы здоровья, кнопки, отладка).
/// </summary>
public static class SpriteManager
{
    /// <summary>Спрайт базового врага (загружается из Content/monster.png)</summary>
    public static Texture2D BaseEnemySprite { get; private set; }
    
    /// <summary>Спрайт цитадели (загружается из Content/citadel.png)</summary>
    public static Texture2D CitadelSprite { get; private set; }
    
    /// <summary>Спрайт башни (загружается из Content/tower.png)</summary>
    public static Texture2D OldTowerSprite { get; private set; }
    
    public static Texture2D StoneTowerSprite { get; private set; }
    
    public static Texture2D FireTowerSprite { get; private set; }
    
    public static Texture2D LightTowerSprite { get; private set; }
    
    public static Texture2D PoisonTowerSprite { get; private set; }
    
    public static Texture2D IceTowerSprite { get; private set; }
    
    public static Texture2D GroundSprite { get; private set; }
    
    /// <summary>Спрайт стены (загружается из Content/wall.png, пока не используется)</summary>
    public static Texture2D WallSprite { get; private set; }
    
    /// <summary>Текстура для полосы здоровья (однопиксельная, создается программно)</summary>
    public static Texture2D HealthBarSprite { get; private set; }
    
    /// <summary>Спрайт снаряда типа Orb (синий круг, создается программно)</summary>
    public static Texture2D OrbProjectileSprite { get; private set; }
    
    /// <summary>Текстура фона кнопок (серый квадрат 64x64, создается программно)</summary>
    public static Texture2D ButtonBackgroundTexture { get; private set; }
    
    /// <summary>Текстура для отладочной отрисовки (зеленый пиксель, создается программно)</summary>
    public static Texture2D GreenDebugPen { get; private set; }
    
    /// <summary>Текстура главного меню (загружается из Content/main_menu.jpg)</summary>
    public static Texture2D MainMenuBackground { get; private set; }
    
    /// <summary>Текстура кнопок меню (загружается из Content/menu_button.png)</summary>
    public static Texture2D MenuButtonTexture { get; private set; }
    
    /// <summary>Текстура фона для меню выбора башен (загружается из Content/paper-horiz)</summary>
    public static Texture2D PaperHorizTexture { get; private set; }
    
    /// <summary>Шрифт по умолчанию для отрисовки текста (загружается из Content/DefaultFont.spritefont)</summary>
    public static SpriteFont DefaultFont { get; private set; }

    /// <summary>
    /// Загрузка всех спрайтов и шрифтов. Вызывается при инициализации игры.
    /// Загружает текстуры из Content и создает программные текстуры.
    /// </summary>
    /// <param name="contentManager">ContentManager для загрузки ресурсов из Content</param>
    /// <param name="graphicsDevice">GraphicsDevice для создания программных текстур</param>
    public static void LoadSprites(ContentManager contentManager, GraphicsDevice graphicsDevice)
    {
        // Загрузка текстур из файлов Content
        BaseEnemySprite = contentManager.Load<Texture2D>("monster");
        CitadelSprite = contentManager.Load<Texture2D>("citadel");
        OldTowerSprite = contentManager.Load<Texture2D>("tower");
        FireTowerSprite = contentManager.Load<Texture2D>("fire_tower");
        IceTowerSprite = contentManager.Load<Texture2D>("ice-tower");
        LightTowerSprite = contentManager.Load<Texture2D>("light_tower");
        StoneTowerSprite = contentManager.Load<Texture2D>("stone_tower");
        PoisonTowerSprite = contentManager.Load<Texture2D>("poison_tower");
        GroundSprite = contentManager.Load<Texture2D>("ground");
        WallSprite = contentManager.Load<Texture2D>("wall");
        MainMenuBackground = contentManager.Load<Texture2D>("main_menu");
        MenuButtonTexture = contentManager.Load<Texture2D>("menu_button");
        PaperHorizTexture = contentManager.Load<Texture2D>("paper-horiz");
        
        // Создание программных текстур с использованием TextureHelper из GameKit
        HealthBarSprite = TextureHelper.CreatePixel(graphicsDevice, Color.White); // Однопиксельная белая текстура
        OrbProjectileSprite = TextureHelper.CreateCircle(graphicsDevice, 16, 16, Color.Blue, Color.Transparent, filled: true); // Синий круг диаметром 16
        ButtonBackgroundTexture = TextureHelper.CreateSolidColor(graphicsDevice, 64, 64, new Color(200, 200, 200)); // Серый квадрат 64x64
        GreenDebugPen = TextureHelper.CreatePixel(graphicsDevice, Color.Green); // Зеленый пиксель для отладки
        
        // Загрузка шрифта
        DefaultFont = contentManager.Load<SpriteFont>("DefaultFont");
    }

}