namespace Tower.Core.Models.Base;

public abstract class BasePlayer
{
    public int Money { get; set; }
    public int Health { get; set; }

    protected BasePlayer(int initialMoney, int initialHealth)
    {
        Money = initialMoney;
        Health = initialHealth;
    }

    public abstract void EarnMoney(int amount);
    public abstract bool SpendMoney(int amount);
    public abstract void TakeDamage(int damage);
}