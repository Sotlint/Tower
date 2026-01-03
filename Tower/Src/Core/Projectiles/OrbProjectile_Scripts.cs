using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameKit2D.Abstractions.Interfaces.GameObjects;

namespace Tower.Core.Projectiles;

/// <summary>
/// Скрипты для снаряда типа "Orb" (сфера). Реализует логику движения, атаки и отрисовки снаряда.
/// Снаряд летит к цели (врагу) и наносит урон при достижении.
/// </summary>
public partial class OrbProjectile : IProjectile
{
    /// <summary>
    /// Движение снаряда к указанной позиции. Вычисляет направление и перемещает снаряд.
    /// </summary>
    /// <param name="targetPosition">Целевая позиция (позиция врага)</param>
    public void Move(Vector2 targetPosition)
    {
        // Вычисляем направление к цели
        var direction = targetPosition - Position;

        // Проверка, чтобы не делить на 0 (если цель уже достигнута)
        if (direction.Length() > 0)
        {
            // Нормализуем направление (получаем единичный вектор)
            direction = Vector2.Normalize(direction);
            // Перемещаем снаряд в направлении цели со скоростью Speed
            AddPosition(direction * Speed);
        }
    }

    /// <summary>
    /// Установка позиции снаряда. Устанавливает абсолютную позицию (для GameKit2D).
    /// </summary>
    /// <param name="direction">Новая позиция</param>
    public void SetPosition(Vector2 direction)
    {
        Position = direction;
    }
    
    /// <summary>
    /// Движение снаряда с добавлением смещения (для внутреннего использования в Move).
    /// </summary>
    private void AddPosition(Vector2 offset)
    {
        Position += offset;
    }

    /// <summary>
    /// Атака целей. Наносит урон всем указанным целям и помечает снаряд как использованный.
    /// </summary>
    /// <param name="targets">Список целей для атаки</param>
    public void Attack(IEnumerable<ICanDie> targets)
    {
        // Наносим урон каждой цели
        foreach (var target in targets)
        {
            target?.TakeDamage(AttackPower);
        }
        // Помечаем снаряд как использованный (будет удален в следующем кадре)
        IsAttacked = true;
    }

    /// <summary>
    /// Отрисовка снаряда. Отрисовывает спрайт снаряда, если цель еще жива.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="gameTime">Время игры</param>
    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Отрисовываем снаряд только если цель еще жива
        if (!Target.IsDie())
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront); // Используем сортировку по глубине
            spriteBatch.Draw(
                Sprite, // Текстура снаряда
                Position, // Позиция снаряда
                null, // Область текстуры (null = вся текстура)
                Color.White, // Цвет (без изменений)
                0f, // Угол поворота
                Vector2.Zero, // Точка привязки (верхний левый угол)
                SpriteScale, // Масштаб спрайта
                SpriteEffects.None, // Эффекты отображения (без эффектов)
                Renderers.TileRenderer.LayerDepth.Projectiles // Глубина слоя (уровень 2)
            );
            spriteBatch.End();
        }
    }

    /// <summary>
    /// Обновление снаряда. Вызывается каждый кадр.
    /// Проверяет расстояние до цели и либо атакует, либо продолжает движение.
    /// </summary>
    /// <param name="gameTime">Время игры для синхронизации</param>
    public void Update(GameTime gameTime)
    {
        // Обновляем позицию цели (для GameKit2D.IProjectile)
        if (Target != null)
        {
            TargetPosition = Target.Position;
        }
        
        // Обновляем снаряд только если цель жива и снаряд еще не попал
        if (Target != null && !Target.IsDie() && !IsAttacked)
        {
            // Проверяем, достиг ли снаряд цели (в пределах радиуса атаки)
            if (Vector2.Distance(Position, Target.Position) <= AttackRange)
            {
                // Снаряд достиг цели - наносим урон
                Attack(new List<ICanDie>(new[] { Target }));
            }
            else
            {
                // Снаряд еще не достиг цели - продолжаем движение
                Move(Target.Position);
            }
        }
    }
}