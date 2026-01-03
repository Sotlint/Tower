using GameKit2D.Abstractions.Interfaces.GameObjects;

namespace Tower.Core.Abstractions.Base;

/// <summary>
/// Интерфейс снаряда. Использует интерфейс из GameKit2D.
/// Снаряды создаются башнями при атаке врагов и летят к цели.
/// </summary>
public interface IProjectile : GameKit2D.Abstractions.Interfaces.GameObjects.IProjectile, IHaveIdentity
{
    /// <summary>Флаг, указывающий, попал ли снаряд в цель</summary>
    new bool IsAttacked { get; }
}