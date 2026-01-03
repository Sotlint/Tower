using System;
using System.Linq;
using GameKit2D.Abstractions.Interfaces;
using GameKit2D.Abstractions.Interfaces.GameObjects;
using GameKit2D.Core;
using Microsoft.Xna.Framework;

namespace Tower.Systems;

/// <summary>
/// Система для удаления снарядов. Отслеживает снаряды, которые попали в цель или у которых цель уничтожена.
/// </summary>
public class ProjectileCleanupSystem : ISystem, IUpdatableSystem
{
    private readonly GameEngine _gameEngine;

    /// <summary>
    /// Создает новую систему очистки снарядов.
    /// </summary>
    /// <param name="gameEngine">Игровой движок для доступа к объектам</param>
    public ProjectileCleanupSystem(GameEngine gameEngine)
    {
        _gameEngine = gameEngine ?? throw new ArgumentNullException(nameof(gameEngine));
    }

    /// <summary>
    /// Обновление системы. Проверяет снаряды и удаляет те, которые попали в цель или у которых цель уничтожена.
    /// </summary>
    public void Update(GameTime gameTime)
    {
        // Получаем все снаряды из GameEngine
        var projectiles = _gameEngine.GetObjectsWithInterface<IProjectile>().ToList();

        // Проверяем каждый снаряд
        foreach (var projectile in projectiles)
        {
            // Проверяем, нужно ли удалить снаряд:
            // - если цель уничтожена (IsDie)
            // - если снаряд уже попал в цель (IsAttacked)
            if ((projectile.Target.IsDie() || projectile.IsAttacked))
            {
                // Удаляем снаряд
                var converted = (IHaveIdentity)projectile;
                _gameEngine.Remove(converted.Id);
            }
        }
    }

    /// <summary>
    /// Очистка системы.
    /// </summary>
    public void Clear()
    {
        // Нет ресурсов для очистки
    }
}

