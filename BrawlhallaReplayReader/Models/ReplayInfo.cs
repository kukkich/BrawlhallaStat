using System.Text.Json.Serialization;

namespace BrawlhallaReplayReader.Models;

public class ReplayInfo
{
    public int Length { get; set; } = -1;
    public int RandomSeed { get; set; } = -1;
    public int Version { get; set; } = -1;
    public int PlaylistId { get; set; } = -1;
    public string PlaylistName { get; set; } = string.Empty;
    public bool OnlineGame { get; set; } = false;
    public int LevelId { get; set; } = -1;
    public int HeroCount { get; set; } = -1;
    public int EndOfMatchFanfare { get; set; } = 0;
    public GameSettings GameSettings { get; set; } = null!;

    public List<Player> Players { get; set; } = new();
    public Dictionary<int, int> Results { get; set; } = new();
    public List<Face> Deaths { get; set; } = new();
    public List<Face> VictoryFaces { get; set; } = new();
    [JsonIgnore]
    public Dictionary<int, List<Input>> Inputs { get; set; } = new();
}
