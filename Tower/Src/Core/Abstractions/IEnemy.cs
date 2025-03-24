using Tower.Core.Abstractions.Base;
using Tower.Core.Abstractions.Enums;

namespace Tower.Core.Abstractions;

public interface IEnemy : IMovable, ICanAttack, ICanDie, IHaveGameLogic, IHaveDrawLogic, IHaveIdentity, IHaveCollision
{
    EnemyTypeEnum Type { get; }
}