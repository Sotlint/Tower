using System;
using System.Linq;
using GameKit2D.Abstractions.Interfaces;
using GameKit2D.Abstractions.Interfaces.GameObjects;
using GameKit2D.Core;
using Microsoft.Xna.Framework;
using Tower.Core.Abstractions;
using Tower.Managers;

namespace Tower.Systems;

/// <summary>
/// Система для обработки смерти врагов. Отслеживает смерть врагов и начисляет деньги и очки.
/// </summary>
public class EnemyDeathSystem : ISystem, IUpdatableSystem
{
    private readonly GameEngine _gameEngine;

    /// <summary>
    /// Создает новую систему обработки смерти врагов.
    /// </summary>
    /// <param name="gameEngine">Игровой движок для доступа к объектам</param>
    public EnemyDeathSystem(GameEngine gameEngine)
    {
        _gameEngine = gameEngine ?? throw new ArgumentNullException(nameof(gameEngine));
    }

    /// <summary>
    /// Обновление системы. Проверяет смерть врагов и обрабатывает их.
    /// </summary>
    public void Update(GameTime gameTime)
    {
        // Получаем всех врагов из GameEngine
        var enemies = _gameEngine.GetObjectsWithInterface<IEnemy>().ToList();

        // Проверяем каждого врага на смерть
        foreach (var enemy in enemies)
        {
            if (enemy.IsDie())
            {
                // Начисляем игроку деньги за убийство врага
                GameManager.Player.EarnMoney(enemy.Reward);
                
                // Начисляем очки за убийство врага (1 очко за каждого врага)
                GameManager.UpdateScore(1);
                
                // Удаляем мертвого врага
                _gameEngine.Remove(enemy.Id);
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

