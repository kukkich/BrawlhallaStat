namespace BrawlhallaReplayReader;

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

    public void Read(BitStream data)
    {
        Flags = data.ReadInt();
        MaxPlayers = data.ReadInt();
        Duration = data.ReadInt();
        RoundDuration = data.ReadInt();
        StartingLives = data.ReadInt();
        ScoringType = data.ReadInt();
        ScoreToWin = data.ReadInt();
        GameSpeed = data.ReadInt();
        DamageRatio = data.ReadInt();
        LevelSetId = data.ReadInt();
    }

    public void Write(BitStream data)
    {
        data.WriteInt(Flags);
        data.WriteInt(MaxPlayers);
        data.WriteInt(Duration);
        data.WriteInt(RoundDuration);
        data.WriteInt(StartingLives);
        data.WriteInt(ScoringType);
        data.WriteInt(ScoreToWin);
        data.WriteInt(GameSpeed);
        data.WriteInt(DamageRatio);
        data.WriteInt(LevelSetId);
    }

    public static GameSettings FromBitStream(BitStream data)
    {
        var settings = new GameSettings();
        settings.Read(data);
        return settings;
    }
}