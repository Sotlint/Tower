using System;
using System.Linq;
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
        // Обновление цитадели (проверка здоровья)
        GameManager.CitadelManager.UpdateCitadel(gameTime);
        
        // Обновление всех игровых объектов происходит автоматически через GameEngine.Update()
        // (вызывается в MainUpdateLogic перед вызовом WaveUpdateLogic.Update())

        // Проверка окончания волны: все враги уничтожены
        var enemies = GameManager.GameEngine?.GetObjectsWithInterface<Tower.Core.Abstractions.IEnemy>() ?? new System.Collections.Generic.List<Tower.Core.Abstractions.IEnemy>();
        if (!enemies.Any())
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