using System.Text.Json.Serialization;
using BrawlhallaReplayReader.Helpers;

namespace BrawlhallaReplayReader;

public class Replay
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
    public string PlaylistName { get; set; }
    public bool OnlineGame { get; set; } = false;

    public GameSettings GameSettings { get; set; }
    public int LevelId { get; set; } = -1;

    public int HeroCount { get; set; } = -1;
    public List<Entity> Entities { get; set; } = new List<Entity>();

    public int EndOfMatchFanfare { get; set; } = 0;

    private void ReadHeader(BitStream data)
    {
        RandomSeed = data.ReadInt();
        Version = data.ReadInt();
        PlaylistId = data.ReadInt();

        if (PlaylistId != 0)
        {
            PlaylistName = data.ReadString();
        }

        OnlineGame = data.ReadBoolean();
    }

    private void WriteHeader(BitStream data)
    {
        data.WriteInt(RandomSeed);
        data.WriteInt(Version);
        data.WriteInt(PlaylistId);

        if (PlaylistId != 0 && !string.IsNullOrEmpty(PlaylistName))
        {
            data.WriteString(PlaylistName);
        }

        data.WriteBoolean(OnlineGame);
    }

    private void ReadPlayerData(BitStream data)
    {
        GameSettings = GameSettings.FromBitStream(data);
        LevelId = data.ReadInt();

        HeroCount = data.ReadShort();

        Entities.Clear();

        var calculatedChecksum = 0;

        while (data.ReadBoolean())
        {
            var entityId = data.ReadInt();
            var entityName = data.ReadString();

            var playerData = PlayerData.FromBitStream(data, HeroCount);

            Entities.Add(new Entity
            {
                Id = entityId,
                Name = entityName,
                Data = playerData
            });

            calculatedChecksum += playerData.CalcChecksum();
        }

        var secondVersionCheck = data.ReadInt();

        if (secondVersionCheck != Version)
        {
            throw new Exception("Second version check does not match first version check");
        }

        calculatedChecksum += LevelId * 47;

        calculatedChecksum = calculatedChecksum % 173;

        var checksum = data.ReadInt();

        if (checksum != calculatedChecksum)
        {
            Console.WriteLine($"[DEV WARNING] Data checksums don't match: Got {checksum}, calculated {calculatedChecksum}");
        }
    }

    private void WritePlayerData(BitStream data)
    {
        throw new NotImplementedException();
        if (GameSettings == null)
            throw new Exception("Game settings is undefined");

        GameSettings.Write(data);

        data.WriteInt(LevelId);

        //data.WriteShort(HeroCount);

        var checksum = 0;

        foreach (var entity in Entities)
        {
            data.WriteBoolean(true);
            data.WriteInt(entity.Id);
            data.WriteString(entity.Name);

            checksum += entity.Data.CalcChecksum();

            entity.Data.Write(data, HeroCount);
        }

        data.WriteBoolean(false);

        data.WriteInt(Version);

        checksum += LevelId * 47;

        data.WriteInt(checksum % 173);
    }

    private void ReadResults(BitStream data)
    {
        Length = data.ReadInt();
        var thirdVersionCheck = data.ReadInt();

        if (thirdVersionCheck != Version)
        {
            throw new Exception("Third version check does not match first version check");
        }

        if (data.ReadBoolean())
        {
            Results.Clear();
            while (data.ReadBoolean())
            {
                var entityId = data.ReadBits(5);
                int result = data.ReadShort();
                Results[entityId] = result;
            }
        }

        //TODO Make a another implementation without this reading 
        // Through inheritance 
        // Cause fanfare was added in update 7.00
        // fanfare it's phrases like "WOW!", "Obliteration", "Total destruction" at the end of the match
        // TODO Find out which value of field Version means old (<7.00) versions
        EndOfMatchFanfare = data.ReadInt(); // end of match fanfare id
    }
    private void WriteResults(BitStream data)
    {
        throw new NotImplementedException();
        data.WriteInt(Length);
        data.WriteInt(Version);

        data.WriteBoolean(Results.Any());
        if (Results.Any())
        {
            foreach (var result in Results)
            {
                data.WriteBoolean(true);
                data.WriteBits(result.Key, 5);
                //data.WriteShort(result.Value);
            }
            data.WriteBoolean(false);
        }

        data.WriteInt(EndOfMatchFanfare);
    }

    private void ReadInputs(BitStream data)
    {
        Inputs.Clear();
        while (data.ReadBoolean())
        {
            var entityId = data.ReadBits(5);
            var inputCount = data.ReadInt();

            if (!Inputs.ContainsKey(entityId))
            {
                Inputs[entityId] = new List<Input>();
            }

            for (var i = 0; i < inputCount; i++)
            {
                var timestamp = data.ReadInt();
                var inputState = data.ReadBoolean() ? data.ReadBits(14) : 0;

                Inputs[entityId].Add(new Input
                {
                    Timestamp = timestamp,
                    InputState = inputState
                });
            }
        }
    }

    private void WriteInputs(BitStream data)
    {
        foreach (var inputEntry in Inputs)
        {
            int entityId = inputEntry.Key;
            List<Input> inputs = inputEntry.Value;

            data.WriteBoolean(true);
            data.WriteBits(entityId, 5);
            data.WriteInt(inputs.Count);
            foreach (var input in inputs)
            {
                data.WriteInt(input.Timestamp);
                data.WriteBoolean(input.InputState != 0);
                if (input.InputState != 0)
                    data.WriteBits(input.InputState, 14);
            }
        }

        data.WriteBoolean(false);
    }

    private void ReadFaces(BitStream data, bool kos)
    {
        var arr = kos ? Deaths : VictoryFaces;

        while (data.ReadBoolean())
        {
            var entityId = data.ReadBits(5);
            var timestamp = data.ReadInt();

            arr.Add(new Face
            {
                EntityId = entityId,
                Timestamp = timestamp
            });
        }

        arr.Sort((a, b) => a.Timestamp - b.Timestamp);
    }

    private void WriteFaces(BitStream data, bool kos)
    {
        var arr = kos ? Deaths : VictoryFaces;

        foreach (var face in arr)
        {
            data.WriteBoolean(true);

            data.WriteBits(face.EntityId, 5);
            data.WriteInt(face.Timestamp);
        }

        data.WriteBoolean(false);
    }

    private void XorData(BitStream data)
    {
        var buffer = data.Data;
        for (var i = 0; i < buffer.Length; i++)
        {
            buffer[i] ^= XorKey[i % XorKey.Length];
        }
    }

    private void Decompress(BitStream data)
    {
        var buffer = data.Data;

        ZLibAdapter.DecompressData(buffer, out var decompressed);
        //byte[] decompressed = ZlibUtils.Inflate(buffer);

        data.Data = decompressed;
    }

    private void Compress(BitStream data)
    {
        var buffer = data.Data;
        
        ZLibAdapter.CompressData(buffer, out var compressed);
        //byte[] compressed = ZlibUtils.Deflate(buffer);

        data.Data = compressed;
    }
    
    public void Read(BitStream data)
    {
        Decompress(data);
        XorData(data);

        StateOrder.Clear();

        var stop = false;
        while (data.ReadBytesAvailable > 0 && !stop)
        {
            var state = data.ReadBits(3);
            StateOrder.Add(state);

            switch (state)
            {
                case 1:
                    ReadInputs(data);
                    break;
                case 2:
                    stop = true;
                    break;
                case 3:
                    ReadHeader(data);
                    break;
                case 4:
                    ReadPlayerData(data);
                    break;
                case 6:
                    ReadResults(data);
                    break;
                case 5:
                case 7:
                    ReadFaces(data, state == 5);
                    break;
                default:
                    throw new Exception("Unknown replay read state: " + state);
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
        var data = new BitStream(new byte[1024 * 512]);

        var stop = false;
        foreach (var state in StateOrder)
        {
            Console.WriteLine($"Writing state: {state}");
            data.WriteBits(state, 3);

            switch (state)
            {
                case 1:
                    WriteInputs(data);
                    break;
                case 2:
                    stop = true;
                    break;
                case 3:
                    WriteHeader(data);
                    break;
                case 4:
                    WritePlayerData(data);
                    break;
                case 6:
                    WriteResults(data);
                    break;
                case 5:
                case 7:
                    WriteFaces(data, state == 5);
                    break;
                default:
                    throw new Exception("Unknown replay write state: " + state);
            }

            if (stop) break;
        }

        data.Shrink();

        XorData(data);
        Compress(data);

        return data.Data;
    }

    public static Replay ReadReplay(byte[] data)
    {
        var replay = new Replay(); 
        var stream = new BitStream(data);
        replay.Read(stream);
        return replay;
    }

    public static byte[] WriteReplay(Replay replay)
    {
        var stream = new BitStream();
        replay.Write(stream);
        return stream.Data;
    }
}

