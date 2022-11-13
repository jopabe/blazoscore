namespace Jox.Blazoscore;

public record PlayerState(Player Player, IReadOnlyList<int> Scores)
{
    // adding constructors messes with the deserialization unfortunately
    public static PlayerState Blank(Player player) => Blank(player, 0);
    public static PlayerState Blank(Player player, int startingScore)
        => new(player, new int[] { startingScore });

    public PlayerState WithScores(IEnumerable<int> scores)
        => this with { Scores = scores.ToArray() };

    public PlayerState AddScore(int score)
        => WithScores(Scores.Append(score));

    public PlayerState AddZero() => AddScore(0);

    public PlayerState RemoveLastScore()
        => Scores.Count > 1 ? WithScores(Scores.Take(Scores.Count - 1)) : this;

    public PlayerState ResetScores()
        => WithScores(Scores.Take(1));
}
