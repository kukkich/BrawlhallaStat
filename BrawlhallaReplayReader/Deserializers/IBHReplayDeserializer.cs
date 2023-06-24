namespace BrawlhallaReplayReader.Deserializers;

// ReSharper disable once InconsistentNaming
public interface IBHReplayDeserializer
{
    public ReplayInfo Read(byte[] bytes);
}