namespace BrawlhallaReplayReader.Deserializers;

public abstract class ReadingStrategyBase : IReadingStrategy
{
    protected void ReadPlayerData(BitStream stream, Replay destination)
    {

        throw new NotImplementedException(); 
    }

    protected void ReadResults(BitStream stream)
    {
        throw new NotImplementedException();
    }

    protected void ReadInputs(BitStream stream)
    {
        throw new NotImplementedException();
    }

    public abstract Replay Read(BitStream stream, Replay replay);
}