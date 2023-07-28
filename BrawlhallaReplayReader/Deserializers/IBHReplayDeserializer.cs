using BrawlhallaReplayReader.Models;

namespace BrawlhallaReplayReader.Deserializers;

// ReSharper disable once InconsistentNaming
public interface IBHReplayDeserializer
{
    public ReplayInfo Deserialize(byte[] bytes);
    public Task<ReplayInfo> DeserializeAsync(byte[] bytes, CancellationToken? cancellationToken = null);
}