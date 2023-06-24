using System.Text.Json.Serialization;
using BrawlhallaReplayReader.Helpers;

namespace BrawlhallaReplayReader;

public class ReplayInfo
{
    private static readonly byte[] XorKey = new byte[]
    {
        107, 16, 222, 60, 68, 75, 209, 70, 160, 16, 82, 193, 178, 49, 211, 106, 251,
        172, 17, 222, 6, 104, 8, 120, 140, 213, 179, 249, 106, 64, 214, 19, 12, 174,
        157, 197, 212, 107, 84, 114, 252, 87, 93, 26, 6, 115, 194, 81, 75, 176, 201,
        140, 120, 4, 17, 122, 239, 116, 62, 70, 57, 160, 199, 166
    };

    public List<int> StateOrder { get; set; } = new() { 3, 4, 6, 1, 5 }; // Keeps track of the order of state writes

    public int Length { get; set; } = -1;
    public Dictionary<int, int> Results { get; set; } = new Dictionary<int, int>();
    public List<Face> Deaths { get; set; } = new List<Face>();
    public List<Face> VictoryFaces { get; set; } = new List<Face>();

    [JsonIgnore]
    public Dictionary<int, List<Input>> Inputs { get; set; } = new Dictionary<int, List<Input>>();

    public int RandomSeed { get; set; } = -1;
    public int Version { get; set; } = -1;
    public int PlaylistId { get; set; } = -1;
    public string PlaylistName { get; set; } = string.Empty;
    public bool OnlineGame { get; set; } = false;

    public GameSettings GameSettings { get; set; } = null!;
    public int LevelId { get; set; } = -1;

    public int HeroCount { get; set; } = -1;
    public List<Entity> Entities { get; set; } = new List<Entity>();

    public int EndOfMatchFanfare { get; set; } = 0;

    private void ReadHeader(BitStream stream)
    {
        RandomSeed = stream.ReadInt();
        Version = stream.ReadInt();
        PlaylistId = stream.ReadInt();

        if (PlaylistId != 0)
        {
            PlaylistName = stream.ReadString();
        }

        OnlineGame = stream.ReadBoolean();
    }

    private void WriteHeader(BitStream stream)
    {
        stream.WriteInt(RandomSeed);
        stream.WriteInt(Version);
        stream.WriteInt(PlaylistId);

        if (PlaylistId != 0 && !string.IsNullOrEmpty(PlaylistName))
        {
            stream.WriteString(PlaylistName);
        }

        stream.WriteBoolean(OnlineGame);
    }

    private void ReadPlayerData(BitStream stream)
    {
        GameSettings = GameSettings.FromBitStream(stream);
        LevelId = stream.ReadInt();

        HeroCount = stream.ReadShort();

        Entities.Clear();

        var calculatedChecksum = 0;

        while (stream.ReadBoolean())
        {
            var entityId = stream.ReadInt();
            var entityName = stream.ReadString();

            var playerData = PlayerData.FromBitStream(stream, HeroCount);

            Entities.Add(new Entity(
                entityId,
                entityName,
                playerData
           ));

            calculatedChecksum += playerData.CalcChecksum();
        }

        var secondVersionCheck = stream.ReadInt();

        if (secondVersionCheck != Version)
        {
            throw new Exception("Second version check does not match first version check");
        }

        calculatedChecksum += LevelId * 47;

        calculatedChecksum = calculatedChecksum % 173;

        var checksum = stream.ReadInt();

        if (checksum != calculatedChecksum)
        {
            Console.WriteLine($"[DEV WARNING] Data checksums don't match: Got {checksum}, calculated {calculatedChecksum}");
        }
    }

    private void WritePlayerData(BitStream stream)
    {
        throw new NotImplementedException();
        if (GameSettings == null)
            throw new Exception("Game settings is undefined");

        GameSettings.Write(stream);

        stream.WriteInt(LevelId);

        //stream.WriteShort(HeroCount);

        var checksum = 0;

        foreach (var entity in Entities)
        {
            stream.WriteBoolean(true);
            stream.WriteInt(entity.Id);
            stream.WriteString(entity.Name);

            checksum += entity.Data.CalcChecksum();

            entity.Data.Write(stream, HeroCount);
        }

        stream.WriteBoolean(false);

        stream.WriteInt(Version);

        checksum += LevelId * 47;

        stream.WriteInt(checksum % 173);
    }

    private void ReadResults(BitStream stream)
    {
        Length = stream.ReadInt();
        var thirdVersionCheck = stream.ReadInt();

        if (thirdVersionCheck != Version)
        {
            throw new Exception("Third version check does not match first version check");
        }

        if (stream.ReadBoolean())
        {
            Results.Clear();
            while (stream.ReadBoolean())
            {
                var entityId = stream.ReadBits(5);
                int result = stream.ReadShort();
                Results[entityId] = result;
            }
        }

        //TODO Make a another implementation without this reading 
        // Through inheritance 
        // Cause fanfare was added in update 7.00
        // fanfare it's phrases like "WOW!", "Obliteration", "Total destruction" at the end of the match
        // TODO Find out which value of field Version means old (<7.00) versions
        EndOfMatchFanfare = stream.ReadInt(); // end of match fanfare id
    }
    private void WriteResults(BitStream stream)
    {
        throw new NotImplementedException();
        stream.WriteInt(Length);
        stream.WriteInt(Version);

        stream.WriteBoolean(Results.Any());
        if (Results.Any())
        {
            foreach (var result in Results)
            {
                stream.WriteBoolean(true);
                stream.WriteBits(result.Key, 5);
                //stream.WriteShort(result.Value);
            }
            stream.WriteBoolean(false);
        }

        stream.WriteInt(EndOfMatchFanfare);
    }

    private void ReadInputs(BitStream stream)
    {
        Inputs.Clear();
        while (stream.ReadBoolean())
        {
            var entityId = stream.ReadBits(5);
            var inputCount = stream.ReadInt();

            if (!Inputs.ContainsKey(entityId))
            {
                Inputs[entityId] = new List<Input>();
            }

            for (var i = 0; i < inputCount; i++)
            {
                var timestamp = stream.ReadInt();
                var inputState = stream.ReadBoolean() ? stream.ReadBits(14) : 0;

                Inputs[entityId].Add(new Input(
                    timestamp, 
                    inputState
                ));
            }
        }
    }

    private void WriteInputs(BitStream stream)
    {
        foreach (var inputEntry in Inputs)
        {
            int entityId = inputEntry.Key;
            List<Input> inputs = inputEntry.Value;

            stream.WriteBoolean(true);
            stream.WriteBits(entityId, 5);
            stream.WriteInt(inputs.Count);
            foreach (var input in inputs)
            {
                stream.WriteInt(input.Timestamp);
                stream.WriteBoolean(input.InputState != 0);
                if (input.InputState != 0)
                    stream.WriteBits(input.InputState, 14);
            }
        }

        stream.WriteBoolean(false);
    }

    private void ReadFaces(BitStream stream, bool kos)
    {
        var arr = kos ? Deaths : VictoryFaces;

        while (stream.ReadBoolean())
        {
            var entityId = stream.ReadBits(5);
            var timestamp = stream.ReadInt();

            arr.Add(new Face(entityId, timestamp));
        }

        arr.Sort((a, b) => a.Timestamp - b.Timestamp);
    }

    private void WriteFaces(BitStream stream, bool kos)
    {
        var arr = kos ? Deaths : VictoryFaces;

        foreach (var face in arr)
        {
            stream.WriteBoolean(true);

            stream.WriteBits(face.EntityId, 5);
            stream.WriteInt(face.Timestamp);
        }

        stream.WriteBoolean(false);
    }

    private void XorData(BitStream stream)
    {
        var buffer = stream.Data;
        for (var i = 0; i < buffer.Length; i++)
        {
            buffer[i] ^= XorKey[i % XorKey.Length];
        }
    }

    private void Decompress(BitStream stream)
    {
        var buffer = stream.Data;

        ZLibAdapter.DecompressData(buffer, out var decompressed);
        //byte[] decompressed = ZlibUtils.Inflate(buffer);

        stream.Data = decompressed;
    }

    private void Compress(BitStream stream)
    {
        var buffer = stream.Data;

        ZLibAdapter.CompressData(buffer, out var compressed);
        //byte[] compressed = ZlibUtils.Deflate(buffer);

        stream.Data = compressed;
    }

    internal void Read(BitStream stream)
    {
        Decompress(stream);
        XorData(stream);

        StateOrder.Clear();

        var stop = false;
        while (stream.ReadBytesAvailable > 0 && !stop)
        {
            var state = stream.ReadBits(3);
            StateOrder.Add(state);

            switch (state)
            {
                case 1:
                    ReadInputs(stream);
                    break;
                case 2:
                    stop = true;
                    break;
                case 3:
                    ReadHeader(stream);
                    break;
                case 4:
                    ReadPlayerData(stream);
                    break;
                case 6:
                    ReadResults(stream);
                    break;
                case 5:
                case 7:
                    ReadFaces(stream, state == 5);
                    break;
                default:
                    throw new Exception("Unknown replayInfo read state: " + state);
            }
        }
    }

    public byte[] Write(BitStream? stream = null)
    {
        if (stream != null)
        {
            XorData(stream);
            Compress(stream);

            return stream.Data;
        }

        // TODO: dynamically resize
        var bitStream = new BitStream(new byte[1024 * 512]);

        var stop = false;
        foreach (var state in StateOrder)
        {
            Console.WriteLine($"Writing state: {state}");
            bitStream.WriteBits(state, 3);

            switch (state)
            {
                case 1:
                    WriteInputs(bitStream);
                    break;
                case 2:
                    stop = true;
                    break;
                case 3:
                    WriteHeader(bitStream);
                    break;
                case 4:
                    WritePlayerData(bitStream);
                    break;
                case 6:
                    WriteResults(bitStream);
                    break;
                case 5:
                case 7:
                    WriteFaces(bitStream, state == 5);
                    break;
                default:
                    throw new Exception("Unknown replayInfo write state: " + state);
            }

            if (stop) break;
        }

        bitStream.Shrink();

        XorData(bitStream);
        Compress(bitStream);

        return bitStream.Data;
    }

    internal static ReplayInfo ReadReplay(byte[] stream)
    {
        var replay = new ReplayInfo();
        var bitStream = new BitStream(stream);
        replay.Read(bitStream);
        return replay;
    }

    internal static byte[] WriteReplay(ReplayInfo replayInfo)
    {
        var stream = new BitStream();
        replayInfo.Write(stream);
        return stream.Data;
    }
}

