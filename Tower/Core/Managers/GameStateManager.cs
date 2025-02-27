using Tower.Core.Models.Abstractions;

namespace Tower.Core.Managers;

public class GameStateManager
{
    public GameStateEnum CurrentState { get; private set; }
    
    public GameStateEnum? PreviousState { get; private set; }

    public GameStateManager()
    {
        CurrentState = GameStateEnum.Menu;
        PreviousState = null;
    }

    public void ChangeState(GameStateEnum newState)
    {
        PreviousState = CurrentState;
        CurrentState = newState;
    }
}