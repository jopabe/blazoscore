namespace Jox.Blazoscore;

public record PlayerState(Player Player, IReadOnlyList<int> Scores)
{
    public static PlayerState Blank(Player player) => Blank(player, 0);
    public static PlayerState Blank(Player player, int startingScore)
        => new(player, new List<int>() { startingScore });

    public PlayerState AddScore(int score) => this with
    { Scores = Scores.Append(score).ToList() };

    public PlayerState AddZero() => this with
    { Scores = Scores.Append(0).ToList() };

    public PlayerState RemoveLastScore() => this with
    { Scores = Scores.Count > 1 ? Scores.Take(Scores.Count - 1).ToList() : Scores };

    public PlayerState ResetScores() => this with
    { Scores = Scores.Take(1).ToList() };
}
