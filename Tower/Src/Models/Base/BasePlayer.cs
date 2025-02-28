namespace Tower.Models.Base;

public abstract class BasePlayer
{
    public int Money { get; set; }

    protected BasePlayer(int initialMoney)
    {
        Money = initialMoney;
    }

    public abstract void EarnMoney(int amount);
    public abstract bool SpendMoney(int amount);
}