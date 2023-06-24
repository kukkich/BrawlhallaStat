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

    public void Read(BitStream Stream)
    {
        Flags = Stream.ReadInt();
        MaxPlayers = Stream.ReadInt();
        Duration = Stream.ReadInt();
        RoundDuration = Stream.ReadInt();
        StartingLives = Stream.ReadInt();
        ScoringType = Stream.ReadInt();
        ScoreToWin = Stream.ReadInt();
        GameSpeed = Stream.ReadInt();
        DamageRatio = Stream.ReadInt();
        LevelSetId = Stream.ReadInt();
    }

    public void Write(BitStream Stream)
    {
        Stream.WriteInt(Flags);
        Stream.WriteInt(MaxPlayers);
        Stream.WriteInt(Duration);
        Stream.WriteInt(RoundDuration);
        Stream.WriteInt(StartingLives);
        Stream.WriteInt(ScoringType);
        Stream.WriteInt(ScoreToWin);
        Stream.WriteInt(GameSpeed);
        Stream.WriteInt(DamageRatio);
        Stream.WriteInt(LevelSetId);
    }

    public static GameSettings FromBitStream(BitStream Stream)
    {
        var settings = new GameSettings();
        settings.Read(Stream);
        return settings;
    }
}