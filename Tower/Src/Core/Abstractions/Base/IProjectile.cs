namespace Tower.Core.Abstractions.Base;

public interface IProjectile : IMovable, ICanAttack, IHaveDrawLogic, IHaveGameLogic
{
    IEnemy Target { get; }
    bool IsAttacked { get; }
}