using Tower.Src.Models.Abstractions;

namespace Tower.Src.Managers;

public class GameStateManager
{
    public GameStateEnum CurrentState { get; private set; }
    
    public GameStateEnum? PreviousState { get; private set; }

    public GameStateManager()
    {
        CurrentState = GameStateEnum.Planning;
        PreviousState = null;
    }

    public void ChangeState(GameStateEnum newState)
    {
        PreviousState = CurrentState;
        CurrentState = newState;
    }
}