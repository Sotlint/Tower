namespace Tower.Managers;

/// <summary>
/// Менеджер сложности. Управляет уровнем сложности игры.
/// Отвечает за увеличение сложности после каждой волны и определение количества врагов.
/// </summary>
public class DifficultyManager
{
    /// <summary>Текущий уровень сложности (начинается с 1, увеличивается после каждой волны)</summary>
    private int DifficultyLevel { get; set; } = 1;

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
    /// В текущей реализации количество врагов равно уровню сложности.
    /// </summary>
    /// <returns>Количество врагов для спавна</returns>
    public int GetEnemyCount()
        => DifficultyLevel;
}