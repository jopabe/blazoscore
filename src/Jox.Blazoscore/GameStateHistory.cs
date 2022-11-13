using Microsoft.VisualBasic;

namespace Jox.Blazoscore;

public class GameStateHistory : IUndoFunction
{
    public event Action? OnStateChange;

    private Stack<GameState> UndoStack { get; } = new();
    private Stack<GameState> RedoStack { get; } = new();

    public bool IsUndoAvailable => UndoStack.Count > 1;
    public bool IsRedoAvailable => RedoStack.Count > 0;

    public GameState CurrentState => UndoStack.Peek();

    public GameStateHistory(GameState initial)
    {
        UndoStack.Push(initial);
    }

    public bool TryUndo()
    {
        if (!IsUndoAvailable)
        {
            return false;
        }
        RedoStack.Push(UndoStack.Pop());
        NotifyStateChanged();
        return true;
    }
    public bool TryRedo()
    {
        if (!IsRedoAvailable)
        {
            return false;
        }
        UndoStack.Push(RedoStack.Pop());
        NotifyStateChanged();
        return true;
    }
    public void Update(Func<GameState, GameState> change)
    {
        UndoStack.Push(change(CurrentState));
        RedoStack.Clear();
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnStateChange?.Invoke();
}
