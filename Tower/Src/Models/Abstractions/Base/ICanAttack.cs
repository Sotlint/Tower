using System;
using System.Collections.Generic;

namespace Tower.Models.Abstractions.Base;

public interface ICanAttack
{
    int AttackPower { get; }
    float AttackRange { get; }
    TimeSpan AttackDelay { get; }
    void Attack(IEnumerable<ICanDie> targets);
    TimeSpan TimeSinceLastAttack { get; }
}