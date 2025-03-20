namespace Tower.Models.Abstractions.Base;

public interface ICanDie
{
    int Health { get; }

    void TakeDamage(int damage);

    bool IsDie();
}