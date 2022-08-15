using System.Text.Json.Serialization;

namespace Jox.Blazoscore;

public class Game
{
    public event Action? OnStateChange;

    private Stack<GameState> State { get; } = new();
    private Stack<GameState> RedoStack { get; } = new();

    public GameState CurrentState => State.Peek();
    public bool IsUndoAvailable => State.Count > 1;
    public bool IsRedoAvailable => RedoStack.Count > 0;

    public Game(GameState initial)
    {
        State.Push(initial);
    }

    public void AddPlayer(PlayerState playerState)
        => UpdateState(CurrentState.AddPlayer(playerState));

    public void AddPlayer(Player player)
        => UpdateState(CurrentState.AddPlayer(PlayerState.Blank(player)));

    public void RemovePlayer(Player player)
        => UpdateState(CurrentState.RemovePlayer(player));

    public void RenamePlayer(Player player, string newName)
        => UpdateState(CurrentState.RenamePlayer(player, newName));

    public void UpdatePlayerState(PlayerState newState)
        => UpdateState(CurrentState.UpdatePlayer(newState));

    public void ResetScores()
        => UpdateState(CurrentState.ResetScores());

    public void Undo()
    {
        if (IsUndoAvailable)
        {
            RedoStack.Push(State.Pop());
            NotifyStateChanged();
        }
    }

    public void Redo()
    {
        if (IsRedoAvailable)
        {
            State.Push(RedoStack.Pop());
            NotifyStateChanged();
        }
    }

    private void UpdateState(GameState newState)
    {
        State.Push(newState);
        RedoStack.Clear();
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnStateChange?.Invoke();

}
