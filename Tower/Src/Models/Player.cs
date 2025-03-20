namespace Tower.Models;

public class Player
{
    public int Money { get; private set; }
    
    public Player(int initialMoney)
    {
    }

    public void EarnMoney(int amount)
    {
        Money += amount;
    }

    public  bool SpendMoney(int amount)
    {
        if (Money >= amount)
        {
            Money -= amount;
            return true;
        }

        return false;
    }
}