namespace Tower.Managers;

public class DifficultyManager
{
    private int DifficultyLevel { get; set; } = 1;

    public void IncreaseDifficulty()
        => DifficultyLevel++;
    
    public int GetDifficultyLevel()
        => DifficultyLevel;

    public int GetEnemyCount()
        => DifficultyLevel * 5;
}