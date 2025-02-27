namespace Tower.Core.Managers;

public class GameManager
{
    public GameStateManager GameStateManager { get; private set; }
    public TowerManager TowerManager { get; private set; }
    public PathManager PathManager { get; private set; }
    public EnemyManager EnemyManager { get; private set; }
    public DifficultyManager DifficultyManager { get; private set; }
    private int Score { get; set; }

    public void UpdateScore(int points)
    {
        Score += points;
    }

    public void Init()
    {
        Score = 0;
        TowerManager = new TowerManager();
        GameStateManager = new GameStateManager();
        PathManager = new PathManager();
        EnemyManager = new EnemyManager();
        DifficultyManager = new DifficultyManager();
    }
}