using ComponentAce.Compression.Libs.zlib;
using System.Buffers;

namespace BrawlhallaReplayReader.Helpers;

public static class ZLibFacade
{
    private static readonly ArrayPool<byte> BufferPool = ArrayPool<byte>.Shared;

    public static void CompressData(byte[] inData, out byte[] outData)
    {
        using var outMemoryStream = new MemoryStream();
        using var outZStream = new ZOutputStream(outMemoryStream, zlibConst.Z_DEFAULT_COMPRESSION);
        using var inMemoryStream = new MemoryStream(inData);
        var buffer = BufferPool.Rent(2000);

        try
        {
            CopyStream(inMemoryStream, outZStream, buffer);
        }
        finally
        {
            BufferPool.Return(buffer);
        }

        outZStream.finish();
        outData = outMemoryStream.ToArray();
    }

    public static void DecompressData(byte[] inData, out byte[] outData)
    {
        using var outMemoryStream = new MemoryStream();
        using var outZStream = new ZOutputStream(outMemoryStream);
        using var inMemoryStream = new MemoryStream(inData);
        var buffer = BufferPool.Rent(2000);

        try
        {
            CopyStream(inMemoryStream, outZStream, buffer);
        }
        finally
        {
            BufferPool.Return(buffer);
        }

        outZStream.finish();
        outData = outMemoryStream.ToArray();
    }

    private static void CopyStream(MemoryStream input, Stream output, byte[] buffer)
    {
        int len;
        while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
        {
            output.Write(buffer, 0, len);
        }
        output.Flush();
    }
}