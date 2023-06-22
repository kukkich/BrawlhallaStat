namespace BrawlhallaReplayReader.Deserializers;

public interface IReadingStrategy
{
    public Replay Read(BitStream stream, Replay replay);
}