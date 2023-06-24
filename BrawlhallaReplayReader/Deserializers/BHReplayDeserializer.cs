using BrawlhallaReplayReader.Helpers;

namespace BrawlhallaReplayReader.Deserializers;

public class BHReplayDeserializer
{
    private static readonly byte[] XorKey = {
        107, 16, 222, 60, 68, 75, 209, 70, 160, 16, 82, 193, 178, 49, 211, 106, 251,
        172, 17, 222, 6, 104, 8, 120, 140, 213, 179, 249, 106, 64, 214, 19, 12, 174,
        157, 197, 212, 107, 84, 114, 252, 87, 93, 26, 6, 115, 194, 81, 75, 176, 201,
        140, 120, 4, 17, 122, 239, 116, 62, 70, 57, 160, 199, 166
    };
    
    private ReplayInfo _result;
    private IReadingStrategy _readingStrategy;
    
    public ReplayInfo Read(BitStream stream)
    {
        _result = new ReplayInfo();

        Decompress(stream);
        XorData(stream);

        ReadHeader(stream);
        SelectReadingStrategy(stream);
        throw new NotImplementedException();
    }

    private void ReadHeader(BitStream stream)
    {
        var stateCode = stream.ReadBits(3);
        if (stateCode != 3)
        {
            throw new Exception($"Expected state 3 but was {stateCode}");
        }

        _result.RandomSeed = stream.ReadInt();
        _result.Version = stream.ReadInt();
        _result.PlaylistId = stream.ReadInt();

        if (_result.PlaylistId != 0)
        {
            _result.PlaylistName = stream.ReadString();
        }

        _result.OnlineGame = stream.ReadBoolean();
    }

    private void SelectReadingStrategy(BitStream stream)
    {
        //TODO correct versions
        if (_result.Version > 215)
        {
            _readingStrategy = new ReadingStrategyAfterV7(stream);
        }
        else
        {
            _readingStrategy = new ReadingStrategyBeforeV7(stream);
        }
    }

    private void XorData(BitStream stream)
    {
        var buffer = stream.Data;
        for (var i = 0; i < buffer.Length; i++)
            buffer[i] ^= XorKey[i % XorKey.Length];
    }

    private void Decompress(BitStream stream)
    {
        var buffer = stream.Data;

        ZLibAdapter.DecompressData(buffer, out var decompressed);

        stream.Data = decompressed;
    }

    private void Compress(BitStream stream)
    {
        var buffer = stream.Data;

        ZLibAdapter.CompressData(buffer, out var compressed);

        stream.Data = compressed;
    }
}