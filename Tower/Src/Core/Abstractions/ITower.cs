using Tower.Core.Abstractions.Enums;

namespace Tower.Core.Abstractions;

public interface ITower : IBuilding
{
    TowerTypeEnum Type { get; }
}