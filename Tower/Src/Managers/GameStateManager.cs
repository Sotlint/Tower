using Tower.Models.Abstractions;

namespace Tower.Managers;

public class GameStateManager
{
    public GameStateEnum CurrentState { get; private set; } = GameStateEnum.Planning;

    public GameStateEnum? PreviousState { get; private set; } = null;

    public void ChangeState(GameStateEnum newState)
    {
        PreviousState = CurrentState;
        CurrentState = newState;
    }
}