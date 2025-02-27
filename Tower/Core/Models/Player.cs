using Tower.Core.Models.Base;

namespace Tower.Core.Models;

public class Player : BasePlayer
{
    public Player(int initialMoney, int initialHealth) : base(initialMoney, initialHealth)
    {
    }

    public override void EarnMoney(int amount)
    {
        Money += amount;
    }

    public override bool SpendMoney(int amount)
    {
        if (Money >= amount)
        {
            Money -= amount;
            return true;
        }

        return false;
    }

    public override void TakeDamage(int damage)
    {
        Health -= damage;
    }
}