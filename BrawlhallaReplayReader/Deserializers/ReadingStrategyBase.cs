namespace BrawlhallaReplayReader.Deserializers;

public class ReadingStrategyBase
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
}