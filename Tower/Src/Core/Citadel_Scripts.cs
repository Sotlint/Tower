using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Base;
using Tower.Core.Abstractions.Enums;
using Tower.Core.Helpers;
using Tower.Managers;

namespace Tower.Core;

public partial class Citadel : IBuilding
{
    public void Attack(IEnumerable<ICanDie> targets)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Получение урона цитаделью. Уменьшает здоровье на указанное количество.
    /// </summary>
    /// <param name="damage">Количество урона</param>
    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
    
    /// <summary>
    /// Восстанавливает здоровье цитадели до максимума.
    /// Вызывается при начале новой игры.
    /// </summary>
    public void RestoreHealth()
    {
        Health = MaxHealth;
    }

    /// <summary>
    /// Проверка, уничтожена ли цитадель (здоровье <= 0).
    /// </summary>
    /// <returns>true, если цитадель уничтожена, false - иначе</returns>
    public bool IsDie()
        => Health <= 0;

    /// <summary>
    /// Отрисовка цитадели. Отрисовывает спрайт цитадели и полосу здоровья.
    /// В режиме отладки также отрисовывает границы и радиус атаки.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="gameTime">Время игры</param>
    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        spriteBatch.Begin(SpriteSortMode.BackToFront); // Используем сортировку по глубине
        var scale = 0.1f; // Масштаб спрайта цитадели (10% от исходного размера)
        
        // Отрисовка отладочной информации (границы и радиус атаки) в режиме отладки
        if (GameManager.InputManager.IsDebugMode)
        {
            DebugBorderDrawer.DrawDebug(spriteBatch, Bounds, AttackRange, Position);
        }
        
        // Отрисовка спрайта цитадели на уровне 1
        spriteBatch.DrawSprite(Sprite, Position, scale, Renderers.TileRenderer.LayerDepth.Buildings);
        
        // Отрисовка полосы здоровья над цитаделью
        HealthBar.Draw(spriteBatch, gameTime, this);
        spriteBatch.End();
    }

    /// <summary>
    /// Атака цитадели (не реализована, цитадель не атакует).
    /// </summary>
    /// <param name="target">Цель для атаки</param>
    public void Attack(ICanDie target)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Обновление цитадели. Вызывается каждый кадр.
    /// Проверяет, уничтожена ли цитадель, и если да - переводит игру в состояние GameOver.
    /// </summary>
    /// <param name="gameTime">Время игры</param>
    public void Update( GameTime gameTime)
    {
        // Проверяем, уничтожена ли цитадель
        if (IsDie())
        {
            // Устанавливаем финальную статистику перед переходом в GameOver
            var score = GameManager.GetScore(); // Финальный счет
            var wave = GameManager.DifficultyManager.GetDifficultyLevel(); // Номер последней волны
            
            // Передаем статистику в меню окончания игры
            GameManager.UIManager.GameOverMenu.SetGameStats(score, wave);
            
            // Переводим игру в состояние окончания игры
            GameManager.GameStateManager.ChangeState(GameStateEnum.GameOver);
        }
    }

    public void SetPosition(Vector2 direction)
        => Position = direction;

    public void ResolveCollision()
    {
        throw new NotImplementedException();
    }

    public void UpdateBounds()
    {
        var width = (int)(Sprite.Width * 0.1f);
        var height = (int)(Sprite.Height * 0.1f);

        Bounds = new Rectangle(
            (int)(Position.X - width / 2), // Центрирование по X
            (int)(Position.Y - height / 2), // Центрирование по Y
            width,
            height
        );
    }
}