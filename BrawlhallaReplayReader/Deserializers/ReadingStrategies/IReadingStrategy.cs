using BrawlhallaReplayReader.Models;

namespace BrawlhallaReplayReader.Deserializers.ReadingStrategies;

internal interface IReadingStrategy
{
    public ReplayInfo Read(ReplayInfo replayInfo);
}