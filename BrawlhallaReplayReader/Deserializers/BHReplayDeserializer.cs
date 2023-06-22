using BrawlhallaReplayReader.Helpers;
using System.Globalization;

namespace BrawlhallaReplayReader.Deserializers;

public class BHReplayDeserializer
{
    private static readonly byte[] XorKey = {
        107, 16, 222, 60, 68, 75, 209, 70, 160, 16, 82, 193, 178, 49, 211, 106, 251,
        172, 17, 222, 6, 104, 8, 120, 140, 213, 179, 249, 106, 64, 214, 19, 12, 174,
        157, 197, 212, 107, 84, 114, 252, 87, 93, 26, 6, 115, 194, 81, 75, 176, 201,
        140, 120, 4, 17, 122, 239, 116, 62, 70, 57, 160, 199, 166
    };

    public void Read(BitStream data)
    {

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

        data.Data = decompressed;
    }

    private void Compress(BitStream data)
    {
        var buffer = data.Data;

        ZLibAdapter.CompressData(buffer, out var compressed);

        data.Data = compressed;
    }
}