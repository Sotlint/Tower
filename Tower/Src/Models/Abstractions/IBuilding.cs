using Tower.Models.Abstractions.Base;

namespace Tower.Models.Abstractions;

public interface IBuilding : ICanAttack, ICanDie, IHaveGameLogic, IHaveDrawLogic, IHavePosition, IHaveIdentity
{
}