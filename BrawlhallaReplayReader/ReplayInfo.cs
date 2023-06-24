using System.Text.Json.Serialization;
using BrawlhallaReplayReader.Helpers;

namespace BrawlhallaReplayReader;

public class ReplayInfo
{
    public int Length { get; set; } = -1;
    public Dictionary<int, int> Results { get; set; } = new Dictionary<int, int>();
    public List<Face> Deaths { get; set; } = new List<Face>();
    public List<Face> VictoryFaces { get; set; } = new List<Face>();

    [JsonIgnore]
    public Dictionary<int, List<Input>> Inputs { get; set; } = new Dictionary<int, List<Input>>();

    public int RandomSeed { get; set; } = -1;
    public int Version { get; set; } = -1;
    public int PlaylistId { get; set; } = -1;
    public string PlaylistName { get; set; } = string.Empty;
    public bool OnlineGame { get; set; } = false;

    public GameSettings GameSettings { get; set; } = null!;
    public int LevelId { get; set; } = -1;

    public int HeroCount { get; set; } = -1;
    public List<Entity> Entities { get; set; } = new List<Entity>();

    public int EndOfMatchFanfare { get; set; } = 0;
}

