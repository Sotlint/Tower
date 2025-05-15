namespace Tower.Managers;

/// <summary>
/// Менеджер сложности, отвечающий за управление уровнем сложности игры.
/// </summary>
public class DifficultyManager
{
    private int DifficultyLevel { get; set; } = 1;

    public void IncreaseDifficulty()
        => DifficultyLevel++;

    public int GetDifficultyLevel()
        => DifficultyLevel;

    public int GetEnemyCount()
        => DifficultyLevel;
}