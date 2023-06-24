namespace BrawlhallaReplayReader.Deserializers;

public class ReadingStrategyAfterV7 : ReadingStrategyBase
{
    public ReadingStrategyAfterV7(BitStream stream) 
        : base(stream)
        { }

    protected override void ReadResults(ReplayInfo replayInfo)
    {
        base.ReadResults(replayInfo);

        // fanfare it's phrases like "WOW!", "Obliteration", "Total destruction" at the end of the match
        // TODO Find out which value of field Version means old (<7.00) versions
        replayInfo.EndOfMatchFanfare = Stream.ReadInt(); // end of match fanfare id
    }
}