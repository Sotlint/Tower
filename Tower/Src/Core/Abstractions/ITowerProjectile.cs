using GameKit2D.Abstractions.Interfaces.GameObjects;

namespace Tower.Core.Abstractions;

/// <summary>
/// Интерфейс снаряда. Использует интерфейс из GameKit2D.
/// Снаряды создаются башнями при атаке врагов и летят к цели.
/// </summary>
public interface ITowerProjectile : IProjectile, IHaveIdentity
{
    /// <summary>Флаг, указывающий, попал ли снаряд в цель</summary>
    new bool IsAttacked { get; }
}