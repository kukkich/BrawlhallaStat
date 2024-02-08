using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Domain.Games;

[Owned]
public class GameSettings
{
    public int Flags { get; set; }
    public int MaxPlayers { get; set; }
    public int Duration { get; set; }
    public int RoundDuration { get; set; }
    public int StartingLives { get; set; }
    public int ScoringType { get; set; }
    public int ScoreToWin { get; set; }
    public int GameSpeed { get; set; }
    public int DamageRatio { get; set; }
    public int LevelSetId { get; set; }
}