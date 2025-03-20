using Tower.Models.Abstractions.Enums;

namespace Tower.Models.Abstractions;

public interface ITower : IBuilding
{
    TowerTypeEnum Type { get; }
}