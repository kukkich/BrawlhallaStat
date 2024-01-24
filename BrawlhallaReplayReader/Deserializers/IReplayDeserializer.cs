using BrawlhallaReplayReader.Models;

namespace BrawlhallaReplayReader.Deserializers;

// ReSharper disable once InconsistentNaming
public interface IReplayDeserializer
{
    public ReplayInfo Deserialize(byte[] bytes);
}