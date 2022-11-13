using System.Text.Json.Serialization;

namespace Jox.Blazoscore;

public class Game
{
    // event is implemented in GameStateHistory behind the scenes
    public event Action? OnStateChange
    {
        add => States.OnStateChange += value;
        remove => States.OnStateChange -= value;
    }

    private GameStateHistory States { get; }

    public Game(GameState initial)
    {
        States = new(initial);
    }

    public void Do(Func<GameState, GameState> stateChange)
        => States.Update(stateChange);

    public GameState CurrentState => States.CurrentState;
    public IUndoFunction UndoFunction => States;
}
