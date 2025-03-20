using Tower.Models.Abstractions.Base;
using Tower.Models.Abstractions.Enums;

namespace Tower.Models.Abstractions;

public interface IEnemy : IMovable, ICanAttack, ICanDie, IHaveGameLogic, IHaveDrawLogic, IHaveIdentity
{
    EnemyTypeEnum Type { get; }
}