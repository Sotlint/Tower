using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions.Base;

namespace Tower.Managers;

/// <summary>
/// Менеджер снарядов. Управляет коллекцией всех снарядов, выпущенных башнями.
/// Предоставляет методы для добавления, удаления, обновления и отрисовки снарядов.
/// </summary>
public class ProjectileManager
{
    /// <summary>Список всех активных снарядов на карте</summary>
    private List<IProjectile> Projectiles { get; set; } = new();

    /// <summary>
    /// Добавить снаряд на карту. Вызывается башнями при атаке врагов.
    /// </summary>
    /// <param name="projectile">Снаряд для добавления</param>
    public void AddProjectile(IProjectile projectile)
        => Projectiles.Add(projectile);

    /// <summary>
    /// Удалить снаряд с карты (внутренний метод).
    /// </summary>
    /// <param name="projectile">Снаряд для удаления</param>
    private void RemoveProjectile(IProjectile projectile)
    {
        Projectiles.Remove(projectile);
    }

    /// <summary>
    /// Обновление всех снарядов. Вызывается каждый кадр во время активной волны.
    /// Обновляет движение снарядов, проверяет попадание в цели и удаляет попавшие/неактивные снаряды.
    /// </summary>
    /// <param name="gameTime">Время игры для синхронизации</param>
    public void UpdateProjectiles(GameTime gameTime)
    {
        // Проходим по списку снарядов в обратном порядке для безопасного удаления
        for (var i = Projectiles.Count - 1; i >= 0; i--)
        {
            // Проверяем, нужно ли удалить снаряд:
            // - если цель уничтожена (IsDie)
            // - если снаряд уже попал в цель (IsAttacked)
            if (Projectiles[i].Target.IsDie() || Projectiles[i].IsAttacked)
            {
                RemoveProjectile(Projectiles[i]);
            }
            else
            {
                // Обновляем активный снаряд (движение к цели, проверка попадания)
                Projectiles[i].Update(gameTime);
            }
        }
    }

    /// <summary>
    /// Отрисовка всех снарядов на карте.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="gameTime">Время игры</param>
    public void DrawProjectiles(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Отрисовываем каждый снаряд
        foreach (var projectile in Projectiles)
        {
            projectile.Draw(spriteBatch, gameTime);
        }
    }
}