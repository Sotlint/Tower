using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Tower.Managers;

/// <summary>
/// Менеджер точек спавна врагов. Управляет точками появления врагов на карте.
/// Создает точки спавна по краям экрана и предоставляет случайные точки для спавна врагов.
/// </summary>
public class SpawnPointManager
{
    /// <summary>Список всех точек спавна на карте</summary>
    private List<Vector2> _spawnPoints = new();
    
    /// <summary>Генератор случайных чисел для выбора точек спавна</summary>
    private Random _random = new Random();
    
    /// <summary>План спавна для следующей волны: точка спавна -> количество врагов</summary>
    private Dictionary<Vector2, int> _nextWaveSpawnPlan = new();
    
    /// <summary>
    /// Инициализация точек спавна. Создает точки только в углах экрана.
    /// </summary>
    /// <param name="screenWidth">Ширина экрана</param>
    /// <param name="screenHeight">Высота экрана</param>
    public void Initialize(int screenWidth, int screenHeight)
    {
        _spawnPoints.Clear();
        
        // Расстояние от края экрана для точек спавна
        const int margin = 50;
        
        // Создаем точки спавна только в углах экрана
        // Верхний левый угол
        _spawnPoints.Add(new Vector2(margin, margin));
        
        // Верхний правый угол
        _spawnPoints.Add(new Vector2(screenWidth - margin, margin));
        
        // Нижний правый угол
        _spawnPoints.Add(new Vector2(screenWidth - margin, screenHeight - margin));
        
        // Нижний левый угол
        _spawnPoints.Add(new Vector2(margin, screenHeight - margin));
    }
    
    /// <summary>
    /// Получить случайную точку спавна.
    /// </summary>
    /// <returns>Случайная точка спавна из списка доступных точек</returns>
    public Vector2 GetRandomSpawnPoint()
    {
        if (_spawnPoints.Count == 0)
        {
            // Если точки спавна не инициализированы, возвращаем точку по умолчанию
            return new Vector2(10, 10);
        }
        
        // Выбираем случайную точку из списка
        var randomIndex = _random.Next(_spawnPoints.Count);
        return _spawnPoints[randomIndex];
    }
    
    /// <summary>
    /// Получить все точки спавна (для отладки).
    /// </summary>
    /// <returns>Список всех точек спавна</returns>
    public List<Vector2> GetAllSpawnPoints() => _spawnPoints.ToList();
    
    /// <summary>
    /// Планирование следующей волны. Распределяет врагов по точкам спавна.
    /// Вызывается при переходе в фазу планирования.
    /// </summary>
    /// <param name="totalEnemyCount">Общее количество врагов для следующей волны</param>
    public void PlanNextWave(int totalEnemyCount)
    {
        _nextWaveSpawnPlan.Clear();
        
        if (_spawnPoints.Count == 0 || totalEnemyCount <= 0)
            return;
        
        // Выбираем случайные активные точки спавна (от 1 до всех доступных)
        var activePointCount = _random.Next(1, Math.Min(_spawnPoints.Count + 1, totalEnemyCount + 1));
        
        // Перемешиваем точки и выбираем случайные
        var shuffledPoints = _spawnPoints.OrderBy(x => _random.Next()).Take(activePointCount).ToList();
        
        // Распределяем врагов по активным точкам
        var enemiesPerPoint = totalEnemyCount / activePointCount;
        var remainder = totalEnemyCount % activePointCount;
        
        for (int i = 0; i < shuffledPoints.Count; i++)
        {
            var point = shuffledPoints[i];
            // Распределяем остаток по первым точкам
            var count = enemiesPerPoint + (i < remainder ? 1 : 0);
            _nextWaveSpawnPlan[point] = count;
        }
    }
    
    /// <summary>
    /// Получить план спавна для следующей волны.
    /// </summary>
    /// <returns>Словарь: точка спавна -> количество врагов</returns>
    public Dictionary<Vector2, int> GetNextWaveSpawnPlan() => new Dictionary<Vector2, int>(_nextWaveSpawnPlan);
    
    /// <summary>
    /// Получить следующую точку спавна из плана. Используется при создании врагов.
    /// </summary>
    /// <returns>Точка спавна или null, если план пуст</returns>
    public Vector2? GetNextSpawnPointFromPlan()
    {
        if (_nextWaveSpawnPlan.Count == 0)
            return null;
        
        // Возвращаем первую доступную точку из плана
        return _nextWaveSpawnPlan.Keys.First();
    }
    
    /// <summary>
    /// Уменьшить счетчик врагов для точки спавна. Вызывается после создания врага.
    /// </summary>
    /// <param name="spawnPoint">Точка спавна</param>
    public void DecrementSpawnPointCount(Vector2 spawnPoint)
    {
        if (_nextWaveSpawnPlan.ContainsKey(spawnPoint))
        {
            _nextWaveSpawnPlan[spawnPoint]--;
            if (_nextWaveSpawnPlan[spawnPoint] <= 0)
            {
                _nextWaveSpawnPlan.Remove(spawnPoint);
            }
        }
    }
}

