using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core;

namespace Tower.Managers;

/// <summary>
/// Менеджер цитадели. Управляет главной целью врагов - цитаделью.
/// Предоставляет методы для обновления, отрисовки и управления здоровьем цитадели.
/// </summary>
public class CitadelManager
{
    /// <summary>
    /// Конструктор менеджера цитадели. Принимает созданную цитадель.
    /// </summary>
    /// <param name="citadel">Экземпляр цитадели для управления</param>
    public CitadelManager(Citadel citadel)
    {
        Citadel = citadel;
    }

    /// <summary>Экземпляр цитадели, которым управляет этот менеджер</summary>
    private Citadel Citadel { get; set; }

    /// <summary>
    /// Обновление цитадели. Вызывается каждый кадр во время активной волны.
    /// Проверяет здоровье цитадели и переводит игру в GameOver, если цитадель уничтожена.
    /// </summary>
    /// <param name="gameTime">Время игры для синхронизации</param>
    public void UpdateCitadel(GameTime gameTime)
        => Citadel.Update(gameTime);

    /// <summary>
    /// Отрисовка цитадели. Отрисовывает спрайт цитадели и полосу здоровья.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="gameTime">Время игры</param>
    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        => Citadel.Draw(spriteBatch, gameTime);

    /// <summary>
    /// Получить экземпляр цитадели. Используется для доступа к свойствам цитадели
    /// (позиция, здоровье, границы и т.д.) из других систем.
    /// </summary>
    /// <returns>Экземпляр цитадели</returns>
    public Citadel GetCitadel()
        => Citadel;
    
    /// <summary>
    /// Восстанавливает здоровье цитадели до максимума.
    /// Вызывается при начале новой игры для сброса состояния цитадели.
    /// </summary>
    public void RestoreHealth()
    {
        Citadel.RestoreHealth();
    }
}