namespace Tower.Core.Managers;

public class DifficultyManager
{
    private int DifficultyLevel { get; set; }

    public DifficultyManager()
    {
        DifficultyLevel = 1;
    }

    public void IncreaseDifficulty()
        => DifficultyLevel++;
}