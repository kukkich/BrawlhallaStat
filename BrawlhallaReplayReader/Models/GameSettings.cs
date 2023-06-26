namespace BrawlhallaReplayReader.Models;

public class GameSettings
{
    public int Flags { get; set; } = -1;
    public int MaxPlayers { get; set; } = -1;
    public int Duration { get; set; } = -1;
    public int RoundDuration { get; set; } = -1;
    public int StartingLives { get; set; } = -1;
    public int ScoringType { get; set; } = -1;
    public int ScoreToWin { get; set; } = -1;
    public int GameSpeed { get; set; } = -1;
    public int DamageRatio { get; set; } = -1;
    public int LevelSetId { get; set; } = -1;
}