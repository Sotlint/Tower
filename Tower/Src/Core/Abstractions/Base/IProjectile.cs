namespace Tower.Core.Abstractions.Base;

/// <summary>
/// Интерфейс снаряда. Объединяет функциональность движения, атаки, отрисовки и игровой логики.
/// Снаряды создаются башнями при атаке врагов и летят к цели.
/// </summary>
public interface IProjectile : IMovable, ICanAttack, IHaveDrawLogic, IHaveGameLogic
{
    /// <summary>Цель снаряда (враг, в которого стреляют)</summary>
    IEnemy Target { get; }
    
    /// <summary>Флаг, указывающий, попал ли снаряд в цель</summary>
    bool IsAttacked { get; }
}