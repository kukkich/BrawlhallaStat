using BrawlhallaReplayReader.Deserializers.Binary;

namespace BrawlhallaReplayReader.Deserializers.ReadingStrategies;

internal class ReadingStrategyBeforeV7 : ReadingStrategyBase
{
    public ReadingStrategyBeforeV7(BitStream stream)
        : base(stream)
    { }
}