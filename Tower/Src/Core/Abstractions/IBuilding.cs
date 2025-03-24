using Tower.Core.Abstractions.Base;

namespace Tower.Core.Abstractions;

public interface IBuilding : ICanAttack, ICanDie, IHaveGameLogic, IHaveDrawLogic, IHavePosition, IHaveIdentity
{
}