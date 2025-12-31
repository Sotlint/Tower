using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Managers;

namespace Tower.UI;

/// <summary>
/// HUD (Heads-Up Display) игры. Отображает информацию о текущем состоянии игры:
/// счет игрока, количество денег, номер волны и т.д.
/// </summary>
public class GameHUD
{
    /// <summary>
    /// Отрисовка HUD. Отображает счет, деньги и другую информацию в верхнем левом углу экрана.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="gameTime">Время игры</param>
    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Проверяем, что шрифт загружен
        if (SpriteManager.DefaultFont == null)
            return;

        spriteBatch.Begin();

        // Получаем текущие значения
        var score = GameManager.GetScore();
        var money = GameManager.Player.Money;
        var wave = GameManager.DifficultyManager.GetDifficultyLevel();

        // Позиция текста (верхний левый угол с отступом)
        var position = new Vector2(10, 10);
        var lineHeight = 30; // Высота строки
        var textColor = Color.Black; // Цвет текста

        // Отрисовка счета
        var scoreText = $"Очки: {score}";
        spriteBatch.DrawString(SpriteManager.DefaultFont, scoreText, position, textColor);
        position.Y += lineHeight;

        // Отрисовка денег
        var moneyText = $"Деньги: {money}";
        spriteBatch.DrawString(SpriteManager.DefaultFont, moneyText, position, textColor);
        position.Y += lineHeight;

        // Отрисовка номера волны (только во время игры или планирования)
        var state = GameManager.GameStateManager.CurrentState;
        if (state == Core.Abstractions.Enums.GameStateEnum.Playing || 
            state == Core.Abstractions.Enums.GameStateEnum.Planning)
        {
            var waveText = $"Волна: {wave}";
            spriteBatch.DrawString(SpriteManager.DefaultFont, waveText, position, textColor);
        }

        spriteBatch.End();
    }
}

