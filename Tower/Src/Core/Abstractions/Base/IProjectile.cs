namespace Tower.Core.Abstractions.Base;

public interface IProjectile : IMovable, ICanAttack, IHaveDrawLogic, IHaveGameLogic
{
    public IEnemy Target { get; init; }
}