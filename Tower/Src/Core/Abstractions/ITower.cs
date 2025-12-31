using Tower.Core.Abstractions.Enums;

namespace Tower.Core.Abstractions;

/// <summary>
/// Интерфейс башни. Наследуется от IBuilding и добавляет тип башни.
/// Башни размещаются игроком во время фазы планирования и атакуют врагов во время волны.
/// </summary>
public interface ITower : IBuilding
{
    /// <summary>Тип башни (Basic, Fire, Frost, Bomber и т.д.)</summary>
    TowerTypeEnum Type { get; }
}