using System.Collections.Generic;

namespace Tower.Models.Abstractions.Base;

public interface ICanAttack
{
    int AttackPower { get; }
    float AttackRange { get; }

    void Attack(ICollection<ICanDie> targets);
}