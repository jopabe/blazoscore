namespace Jox.Blazoscore;

public record GameState(IReadOnlyList<PlayerState> Players, int StartingScore = 0)
{
    public GameState AddPlayer(PlayerState playerState)
    {
        if (Players.Any(p => p.Player == playerState.Player))
        {
            return this;
        }
        else
        {
            return this with { Players = Players.Append(playerState).ToList() };
        }
    }
    public GameState AddPlayer(Player player) => AddPlayer(PlayerState.Blank(player, StartingScore));

    public GameState RemovePlayer(Player player) => this with
    {
        Players = Players.Where(p => !player.Equals(p.Player)).ToList()
    };

    public GameState UpdatePlayer(PlayerState newState) => this with
    {
        Players = Players.Select(p => p.Player == newState.Player ? newState : p).ToList()
    };

    public GameState RenamePlayer(Player player, string newName) => this with
    {
        Players = Players.Select(p => p.Player == player ?
        p with { Player = player with { Name = newName } } : p).ToList()
    };

    public GameState ResetScores() => this with
    {
        Players = Players.Select(p => p.ResetScores()).ToList()
    };
}
