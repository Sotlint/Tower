using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Managers;

namespace Tower.Renderers;

/// <summary>
/// Рендерер для состояния "Главное меню" (Menu). Отвечает за отрисовку главного меню игры.
/// </summary>
public static class MenuRenderer
{
    /// <summary>
    /// Отрисовка игрового состояния "Главное меню". Отрисовывает только меню,
    /// игровое поле не видно.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="gameTime">Время игры для синхронизации анимаций</param>
    public static void Render(SpriteBatch spriteBatch, GameTime gameTime)
    {
        var menu = GameManager.UIManager.GetActiveMenu();
        // Отрисовываем только меню (игровое поле не видно)
        menu?.Draw(spriteBatch, gameTime);
    }
}

