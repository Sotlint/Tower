using System;
using Microsoft.Xna.Framework;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.LogicUpdaters;

/// <summary>
/// Логика обновления активной волны врагов. Управляет обновлением всех игровых объектов
/// во время волны и проверяет условия окончания волны.
/// </summary>
public static class WaveUpdateLogic
{
    /// <summary>
    /// Обновление логики волны. Вызывается каждый кадр во время активной волны.
    /// Обновляет все игровые объекты и проверяет, закончилась ли волна.
    /// </summary>
    /// <param name="gameTime">Время игры для синхронизации обновлений</param>
    public static void Update(GameTime gameTime)
    {
        // Обновление всех игровых объектов во время волны
        GameManager.CitadelManager.UpdateCitadel(gameTime); // Обновление цитадели (проверка здоровья)
        GameManager.TowerManager.UpdateTowers(gameTime); // Обновление башен (поиск целей, атака)
        GameManager.ProjectileManager.UpdateProjectiles(gameTime); // Обновление снарядов (движение, попадание)
        GameManager.EnemyManager.UpdateEnemies(gameTime); // Обновление врагов (движение, атака цитадели)

        // Проверка окончания волны: все враги уничтожены
        if (GameManager.EnemyManager.GetEnemies().Count == 0)
        {
            // Увеличиваем сложность для следующей волны
            GameManager.DifficultyManager.IncreaseDifficulty();
            
            // Сбрасываем состояние планирования (очищаем временные данные перетаскивания)
            PlannedUpdateLogic.Reset();
            
            // Переводим игру в фазу планирования для подготовки к следующей волне
            GameManager.GameStateManager.ChangeState(GameStateEnum.Planning);
        }
    }
}