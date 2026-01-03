using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Enums;
using Tower.Core.Common;
using Tower.Core.Enemies;
using Tower.Managers;

namespace Tower.Factories;

/// <summary>
/// Фабрика для создания врагов. Предоставляет методы для создания врагов различных типов.
/// Враги спавнятся в случайных точках по краям экрана.
/// </summary>
public static class EnemyFactory
{
    /// <summary>
    /// Создать врагов указанного типа в указанном количестве.
    /// Каждый враг спавнится в случайной точке по краям экрана.
    /// </summary>
    /// <param name="type">Тип врага для создания</param>
    /// <param name="count">Количество врагов для создания</param>
    /// <returns>Список созданных врагов</returns>
    /// <exception cref="NotImplementedException">Если тип врага еще не реализован</exception>
    /// <exception cref="ArgumentOutOfRangeException">Если передан неизвестный тип врага</exception>
    public static List<IEnemy> CreateEnemy(EnemyTypeEnum type, int count)
        => type switch
        {
            EnemyTypeEnum.Basic => CreateBasicEnemy(count, SpriteManager.BaseEnemySprite),
            EnemyTypeEnum.Fast => throw new NotImplementedException(),
            EnemyTypeEnum.Armored => throw new NotImplementedException(),
            EnemyTypeEnum.Boss => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

    /// <summary>
    /// Создать базовых врагов. Базовый враг имеет стандартные характеристики:
    /// здоровье 50, скорость 2, урон 10, радиус атаки 50, задержка атаки 1000мс.
    /// Враги спавнятся согласно плану следующей волны.
    /// </summary>
    /// <param name="count">Количество врагов для создания</param>
    /// <param name="sprite">Текстура спрайта врага</param>
    /// <returns>Список созданных базовых врагов</returns>
    private static List<IEnemy> CreateBasicEnemy(int count, Texture2D sprite)
    {
        var enemies = new List<IEnemy>();
        
        // Получаем менеджер точек спавна для использования плана
        var spawnManager = GameManager.SpawnPointManager;
        
        if (spawnManager == null)
        {
            // Если менеджер недоступен, создаем врагов в точке по умолчанию
            for (var i = 0; i < count; i++)
            {
                enemies.Add(CreateSingleEnemy(new Vector2(10, 10), sprite));
            }
            return enemies;
        }
        
        // Получаем план спавна для следующей волны
        var spawnPlan = spawnManager.GetNextWaveSpawnPlan();
        
        if (spawnPlan.Count == 0)
        {
            // Если план пуст, используем случайные точки
            for (var i = 0; i < count; i++)
            {
                var spawnPosition = spawnManager.GetRandomSpawnPoint();
                enemies.Add(CreateSingleEnemy(spawnPosition, sprite));
            }
            return enemies;
        }
        
        // Создаем врагов согласно плану: для каждой точки создаем указанное количество врагов
        foreach (var kvp in spawnPlan)
        {
            var spawnPoint = kvp.Key;
            var enemyCount = kvp.Value;
            
            for (var i = 0; i < enemyCount; i++)
            {
                enemies.Add(CreateSingleEnemy(spawnPoint, sprite));
            }
        }

        return enemies;
    }
    
    /// <summary>
    /// Создать одного врага в указанной позиции.
    /// </summary>
    /// <param name="spawnPosition">Позиция спавна врага</param>
    /// <param name="sprite">Текстура спрайта врага</param>
    /// <returns>Созданный враг</returns>
    private static IEnemy CreateSingleEnemy(Vector2 spawnPosition, Texture2D sprite)
    {
        return new BasicEnemy(
            sprite, // Текстура спрайта
            Guid.NewGuid(), // Уникальный идентификатор
            spawnPosition, // Позиция спавна
            50m, // Здоровье
            3, // Скорость движения
            10m, // Сила атаки
            70,// Радиус атаки
            EnemyTypeEnum.Basic, // Тип врага
            TimeSpan.FromMilliseconds(1000), // Задержка между атаками (1000мс)
            new HealthBar(SpriteManager.HealthBarSprite) // Полоса здоровья
        );
    }
}