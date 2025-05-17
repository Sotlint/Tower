namespace Tower.Core.Abstractions.Base;

public interface ICanDie
{
    int MaxHealth { get; }
    int Health { get; }

    void TakeDamage(int damage);

    bool IsDie();
}