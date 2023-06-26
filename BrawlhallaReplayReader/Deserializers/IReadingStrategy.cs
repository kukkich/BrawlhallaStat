namespace BrawlhallaReplayReader.Deserializers;

public interface IReadingStrategy
{
    public ReplayInfo Read(ReplayInfo replayInfo);
}