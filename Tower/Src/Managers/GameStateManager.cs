
using Tower.Core.Abstractions.Enums;

namespace Tower.Managers;

/// <summary>
/// Хранит в себе текущее состояние и предыдущее состояние игровой сессии.
/// Предоставляет функциональность для смены состояния игры и отслеживания переходов между состояниями.
/// </summary>
public class GameStateManager
{
    public GameStateEnum CurrentState { get; private set; } = GameStateEnum.Playing;

    public GameStateEnum? PreviousState { get; private set; } = null;

    public void ChangeState(GameStateEnum newState)
    {
        PreviousState = CurrentState;
        CurrentState = newState;
    }
}