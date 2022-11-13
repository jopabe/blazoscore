namespace Jox.Blazoscore
{
    public interface IUndoFunction
    {
        bool IsRedoAvailable { get; }
        bool IsUndoAvailable { get; }

        bool TryRedo();
        bool TryUndo();
    }
}
