namespace BrawlhallaReplayReader.Deserializers;

public abstract class ReadingStrategyBase : IReadingStrategy
{
    protected BitStream Stream { get; set; }

    protected ReadingStrategyBase(BitStream stream)
    {
        Stream = stream;
    }

    protected virtual void ReadPlayerData(ReplayInfo destination)
    {
        destination.GameSettings = GameSettings.FromBitStream(Stream); //TODO extract
        
        destination.LevelId = Stream.ReadInt();
        destination.HeroCount = Stream.ReadShort();
        destination.Entities.Clear();

        var calculatedChecksum = 0;

        while (Stream.ReadBoolean())
        {
            var entityId = Stream.ReadInt();
            var entityName = Stream.ReadString();

            var playerData = PlayerData.FromBitStream(Stream, destination.HeroCount);//TODO extract

            destination.Entities.Add(new Entity(
                entityId,
                entityName,
                playerData
            ));

            calculatedChecksum += playerData.CalcChecksum();
        }

        var secondVersionCheck = Stream.ReadInt();

        if (secondVersionCheck != destination.Version)
        {
            throw new Exception("Second version check does not match first version check");
        }

        calculatedChecksum += destination.LevelId * 47;
        calculatedChecksum %= 173;

        var checksum = Stream.ReadInt();

        if (checksum != calculatedChecksum)
        {
            //TODO extract into logger
            Console.WriteLine($"[DEV WARNING] Data checksums don't match: Got {checksum}, calculated {calculatedChecksum}");
        }
    }

    protected virtual void ReadResults(ReplayInfo destination)
    {
        destination.Length = Stream.ReadInt();
        var thirdVersionCheck = Stream.ReadInt();

        if (thirdVersionCheck != destination.Version)
        {
            throw new Exception("Third version check does not match first version check");
        }

        if (!Stream.ReadBoolean()) return;
        
        destination.Results.Clear();
        
        while (Stream.ReadBoolean())
        {
            var entityId = Stream.ReadBits(5);
            int result = Stream.ReadShort();
            destination.Results[entityId] = result;
        }
    }

    protected virtual void ReadInputs(ReplayInfo destination)
    {
        destination.Inputs.Clear();
        while (Stream.ReadBoolean())
        {
            var entityId = Stream.ReadBits(5);
            var inputCount = Stream.ReadInt();

            if (!destination.Inputs.ContainsKey(entityId))
            {
                destination.Inputs[entityId] = new List<Input>();
            }

            for (var i = 0; i < inputCount; i++)
            {
                var timestamp = Stream.ReadInt();
                var inputState = Stream.ReadBoolean() ? Stream.ReadBits(14) : 0;
                destination.Inputs[entityId].Add(new Input(
                    timestamp,
                    inputState
                ));
            }
        }
    }

    protected virtual void ReadFaces(ReplayInfo destination, bool kos)
    {
        var arr = kos ? destination.Deaths : destination.VictoryFaces;

        while (Stream.ReadBoolean())
        {
            var entityId = Stream.ReadBits(5);
            var timestamp = Stream.ReadInt();

            arr.Add(new Face(entityId, timestamp));
        }

        arr.Sort((a, b) => a.Timestamp - b.Timestamp);
    }

    protected virtual int ReadStateCode() => Stream.ReadBits(3);

    public virtual ReplayInfo Read(ReplayInfo destination)
    {
        var stop = false;
        while (Stream.ReadBytesAvailable > 0 && !stop)
        {
            var state = ReadStateCode();

            switch (state)
            {
                case 1:
                    ReadInputs(destination);
                    break;
                case 2:
                    stop = true;
                    break;
                case 3:
                    //ReadHeader(stream);
                    break;
                case 4:
                    ReadPlayerData(destination);
                    break;
                case 6:
                    ReadResults(destination);
                    break;
                case 5:
                case 7:
                    ReadFaces(destination, state == 5);
                    break;
                default:
                    throw new Exception("Unknown replayInfo read state: " + state);
            }
        }

        return destination;
    }
}