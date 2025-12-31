using Tower.Core.Abstractions.Base;
using Tower.Core.Abstractions.Enums;

namespace Tower.Core.Abstractions;

/// <summary>
/// Интерфейс врага. Объединяет функциональность движения, атаки, здоровья,
/// игровой логики, отрисовки, идентификации и коллизий.
/// Враги движутся к цитадели и атакуют её при достижении.
/// </summary>
public interface IEnemy : IMovable, ICanAttack, ICanDie, IHaveGameLogic, IHaveDrawLogic, IHaveIdentity, IHaveCollision
{
    /// <summary>Тип врага (Basic, Fast, Armored, Boss и т.д.)</summary>
    EnemyTypeEnum Type { get; }
    
    /// <summary>Награда за убийство врага (количество монет, получаемых игроком)</summary>
    int Reward { get; }
}