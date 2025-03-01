using Tower.Models;

namespace Tower.Managers;

public class GameManager
{
    public Player Player { get; private set; }
    public GameStateManager GameStateManager { get; private set; }
    public TowerManager TowerManager { get; private set; }
    public CitadelManager CitadelManager { get; private set; }
    public EnemyManager EnemyManager { get; private set; }
    public DifficultyManager DifficultyManager { get; private set; }
    public SpriteManager SpriteManager { get; private set; }
    private int Score { get; set; }

    public void UpdateScore(int points)
    {
        Score += points;
    }

    public void Init(Citadel citadel)
    {
        Score = 0;
        TowerManager = new TowerManager();
        GameStateManager = new GameStateManager();
        CitadelManager = new CitadelManager(citadel);
        EnemyManager = new EnemyManager();
        DifficultyManager = new DifficultyManager();
        SpriteManager = new SpriteManager();
        Player = new Player(100);
    }
}