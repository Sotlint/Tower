using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Base;
using Tower.Core.Abstractions.Enums;
using Tower.Core.Helpers;
using Tower.Factories;
using Tower.Managers;

namespace Tower.Core.Towers;

/// <summary>
/// Скрипты огненной башни. Реализует логику отрисовки, обновления, атаки и управления границами.
/// </summary>
public partial class FireTower : ITower
{
    /// <summary>
    /// Отрисовка башни. Отрисовывает спрайт башни и отладочную информацию в режиме отладки.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="gameTime">Время игры</param>
    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        spriteBatch.Begin(SpriteSortMode.BackToFront); // Используем сортировку по глубине
        
        // Отрисовка отладочной информации (границы в режиме отладки)
        if (GameManager.InputManager.IsDebugMode)
        {
            DebugBorderDrawer.DrawDebug(spriteBatch, Bounds, AttackRange, Position);
        }
        
        // Отрисовка спрайта башни на уровне 1
        spriteBatch.DrawSprite(Sprite, Position, SpriteScale, Renderers.TileRenderer.LayerDepth.Buildings);
        spriteBatch.End();
    }

    /// <summary>
    /// Обновление башни. Вызывается каждый кадр во время активной волны.
    /// Ищет ближайшего врага в радиусе атаки и стреляет по нему.
    /// </summary>
    /// <param name="gameTime">Время игры для синхронизации</param>
    public void Update(GameTime gameTime)
    {
        // Увеличиваем время с последней атаки
        TimeSinceLastAttack += gameTime.ElapsedGameTime;
        
        // Проверяем, прошла ли задержка между атаками
        if (TimeSinceLastAttack < AttackDelay)
            return;

        // Ищем ближайшего врага в радиусе атаки
        var target = GameManager.EnemyManager.GetEnemies()
            .Where(e => Vector2.Distance(Position, e.Position) <= AttackRange) // Враги в радиусе атаки
            .MinBy(e => Vector2.Distance(Position, e.Position)); // Ближайший враг

        // Если цель найдена - атакуем
        if (target != null)
        {
            Attack(new List<ICanDie>(new[] { target }));
        }

        // Сбрасываем таймер атаки
        TimeSinceLastAttack = TimeSpan.Zero;
    }

    /// <summary>
    /// Атака целей. Создает снаряд для атаки указанных целей.
    /// </summary>
    /// <param name="target">Список целей для атаки</param>
    public void Attack(IEnumerable<ICanDie> target)
    {
        var canDie = target.ToList();
        // Если есть цели - создаем снаряд для атаки первой цели
        if (canDie.Any())
        {
            GameManager.ProjectileManager.AddProjectile(
                ProjectileFactory.CreateProjectile(ProjectileTypeEnum.Orb, Position, (IEnemy)canDie.First()));
        }
    }

    /// <summary>
    /// Получение урона башней. Уменьшает здоровье на указанное количество.
    /// </summary>
    /// <param name="damage">Количество урона</param>
    public void TakeDamage(int damage)
        => Health -= damage;

    /// <summary>
    /// Проверка, уничтожена ли башня (здоровье <= 0).
    /// </summary>
    /// <returns>true, если башня уничтожена, false - иначе</returns>
    public bool IsDie()
        => Health <= 0;

    /// <summary>
    /// Установка позиции башни. Используется при перетаскивании во время планирования.
    /// </summary>
    /// <param name="direction">Новая позиция башни</param>
    public void SetPosition(Vector2 direction)
        => Position = direction;

    /// <summary>
    /// Разрешение коллизий башни (не реализовано, башни статичны).
    /// </summary>
    public void ResolveCollision()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Обновление границ башни. Вычисляет прямоугольник коллизий на основе
    /// позиции и размера спрайта с учетом масштаба.
    /// </summary>
    public void UpdateBounds()
    {
        // Вычисляем размеры с учетом масштаба
        var width = (int)(Sprite.Width * SpriteScale);
        var height = (int)(Sprite.Height * SpriteScale);

        // Создаем прямоугольник с центрированием по позиции
        Bounds = new Rectangle(
            (int)(Position.X - width / 2), // Центрирование по X
            (int)(Position.Y - height / 2), // Центрирование по Y
            width,
            height
        );
    }
}

