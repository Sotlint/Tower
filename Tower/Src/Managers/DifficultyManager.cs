using System;

namespace Tower.Managers;

/// <summary>
/// Менеджер сложности. Управляет уровнем сложности игры.
/// Отвечает за увеличение сложности после каждой волны и определение количества врагов.
/// </summary>
public class DifficultyManager
{
    /// <summary>Текущий уровень сложности (начинается с 1, увеличивается после каждой волны)</summary>
    private int DifficultyLevel { get; set; } = 1;
    
    /// <summary>Генератор случайных чисел для вариации количества врагов</summary>
    private Random _random = new Random();

    /// <summary>
    /// Увеличить уровень сложности. Вызывается после завершения волны.
    /// </summary>
    public void IncreaseDifficulty()
        => DifficultyLevel++;

    /// <summary>
    /// Получить текущий уровень сложности.
    /// </summary>
    /// <returns>Текущий уровень сложности (номер волны)</returns>
    public int GetDifficultyLevel()
        => DifficultyLevel;

    /// <summary>
    /// Получить количество врагов для следующей волны.
    /// Базовое количество равно уровню сложности, с добавлением случайной вариации ±30%.
    /// Минимальное количество врагов - 1.
    /// </summary>
    /// <returns>Количество врагов для спавна (с вариацией)</returns>
    public int GetEnemyCount()
    {
        // Базовое количество врагов равно уровню сложности
        var baseCount = DifficultyLevel;
        
        // Добавляем случайную вариацию ±30% от базового количества
        var variation = (int)(baseCount * 0.3f);
        var randomVariation = _random.Next(-variation, variation + 1);
        
        // Вычисляем итоговое количество с учетом вариации
        var finalCount = baseCount + randomVariation;
        
        // Минимальное количество врагов - 1
        return Math.Max(1, finalCount);
    }
}