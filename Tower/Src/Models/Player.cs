using Tower.Models.Base;

namespace Tower.Models;

public class Player : BasePlayer
{
    public Player(int initialMoney) : base(initialMoney)
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
}