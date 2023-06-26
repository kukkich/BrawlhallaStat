namespace BrawlhallaReplayReader.Models;

public class PlayerData
{
    public int ColorId { get; set; } = -1;
    public int SpawnBotId { get; set; } = -1;
    public int EmitterId { get; set; } = -1;
    public int PlayerThemeId { get; set; } = -1;
    public List<int> Taunts { get; set; } = new();
    public int WinTaunt { get; set; } = -1;
    public int LoseTaunt { get; set; } = -1;
    public List<int> OwnedTaunts { get; set; } = new();
    public int AvatarId { get; set; } = -1;
    public int Team { get; set; } = -1; // 1-red 2-blue
    public int Unknown2 { get; set; } = -1;
    public List<HeroData> Heroes { get; set; } = new();
    public bool Bot { get; set; } = false;

    //Personal settings
    public bool HandicapsEnabled { get; set; } = false;
    public int HandicapStockCount { get; set; } = 3;
    public int HandicapDamageDoneMultiplier { get; set; } = 1;
    public int HandicapDamageTakenMultiplier { get; set; } = 1;
}