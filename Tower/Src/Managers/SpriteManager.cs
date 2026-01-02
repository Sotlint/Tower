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
        
        // Создание программных текстур
        HealthBarSprite = CreateHealthBarTexture(graphicsDevice); // Однопиксельная белая текстура
        OrbProjectileSprite = CreateCircularTexture(graphicsDevice, 8, Color.Blue); // Синий круг радиусом 8
        ButtonBackgroundTexture = CreateButtonBackgroundTexture(graphicsDevice); // Серый квадрат 64x64
        GreenDebugPen = CreateGreenDebugPenTexture(graphicsDevice); // Зеленый пиксель для отладки
        
        // Загрузка шрифта
        DefaultFont = contentManager.Load<SpriteFont>("DefaultFont");
    }

    /// <summary>
    /// Создание текстуры для отладочной отрисовки (зеленый пиксель).
    /// Используется для отрисовки границ и радиусов атаки в режиме отладки.
    /// </summary>
    /// <param name="graphicsDevice">Графическое устройство для создания текстуры</param>
    /// <returns>Однопиксельная зеленая текстура</returns>
    private static Texture2D CreateGreenDebugPenTexture(GraphicsDevice graphicsDevice)
    {
        var pixel = new Texture2D(graphicsDevice, 1, 1);
        pixel.SetData(new[] { Color.Green });
        return pixel;
    }

    /// <summary>
    /// Создание текстуры фона кнопок (серый квадрат 64x64).
    /// Используется как фон для всех кнопок UI.
    /// </summary>
    /// <param name="graphicsDevice">Графическое устройство для создания текстуры</param>
    /// <returns>Серая текстура 64x64 пикселей</returns>
    private static Texture2D CreateButtonBackgroundTexture(GraphicsDevice graphicsDevice)
    {
        var texture = new Texture2D(graphicsDevice, 64, 64);
        var data = new Color[64 * 64];

        // Заполняем все пиксели серым цветом
        for (var i = 0; i < data.Length; i++)
        {
            data[i] = new Color(200, 200, 200); // Светло-серый цвет
        }

        texture.SetData(data);
        return texture;
    }

    /// <summary>
    /// Создание текстуры для полосы здоровья (однопиксельная белая текстура).
    /// Используется для отрисовки полос здоровья врагов и зданий.
    /// </summary>
    /// <param name="graphicsDevice">Графическое устройство для создания текстуры</param>
    /// <returns>Однопиксельная белая текстура</returns>
    private static Texture2D CreateHealthBarTexture(GraphicsDevice graphicsDevice)
    {
        var texture = new Texture2D(graphicsDevice, 1, 1);
        texture.SetData(new[] { Color.White });
        return texture;
    }

    /// <summary>
    /// Создание круглой текстуры указанного радиуса и цвета.
    /// Используется для создания спрайтов снарядов и других круглых объектов.
    /// </summary>
    /// <param name="graphicsDevice">Графическое устройство для создания текстуры</param>
    /// <param name="radius">Радиус круга в пикселях</param>
    /// <param name="color">Цвет круга</param>
    /// <returns>Круглая текстура с прозрачным фоном</returns>
    private static Texture2D CreateCircularTexture(GraphicsDevice graphicsDevice, int radius, Color color)
    {
        var diameter = radius * 2; // Диаметр = радиус * 2
        var texture = new Texture2D(graphicsDevice, diameter, diameter);
        var data = new Color[diameter * diameter];

        // Заполняем текстуру: пиксели внутри круга - указанным цветом, остальные - прозрачные
        for (var y = 0; y < diameter; y++)
        {
            for (var x = 0; x < diameter; x++)
            {
                // Вычисляем расстояние от центра
                var dx = x - radius;
                var dy = y - radius;
                float distanceSquared = dx * dx + dy * dy;

                // Если пиксель внутри круга - красим в указанный цвет
                if (distanceSquared <= radius * radius)
                {
                    data[y * diameter + x] = color;
                }
                else
                {
                    // Иначе - прозрачный
                    data[y * diameter + x] = Color.Transparent;
                }
            }
        }

        texture.SetData(data);
        return texture;
    }
}