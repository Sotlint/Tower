using Tower.Core.Abstractions.Base;

namespace Tower.Core.Abstractions;

/// <summary>
/// Интерфейс здания. Объединяет функциональность атаки, здоровья, игровой логики,
/// отрисовки, позиции, идентификации и коллизий.
/// Здания - это статичные объекты на карте (башни и цитадель).
/// </summary>
public interface IBuilding : ICanAttack, ICanDie, IHaveGameLogic, IHaveDrawLogic, IHavePosition, IHaveIdentity, IHaveCollision
{
}