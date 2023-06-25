using ComponentAce.Compression.Libs.zlib;

namespace BrawlhallaReplayReader.Helpers;

public static class ZLibFacade
{
    public static void CompressData(byte[] inData, out byte[] outData)
    {
        using var outMemoryStream = new MemoryStream();
        using var outZStream = new ZOutputStream(outMemoryStream, zlibConst.Z_DEFAULT_COMPRESSION);
        using MemoryStream inMemoryStream = new (inData);

        CopyStream(inMemoryStream, outZStream);
        outZStream.finish();
        outData = outMemoryStream.ToArray();
    }

    public static void DecompressData(byte[] inData, out byte[] outData)
    {
        using var outMemoryStream = new MemoryStream();
        using var outZStream = new ZOutputStream(outMemoryStream);
        using MemoryStream inMemoryStream = new (inData);

        CopyStream(inMemoryStream, outZStream);
        outZStream.finish();
        outData = outMemoryStream.ToArray();
    }

    private static void CopyStream(MemoryStream input, Stream output)
    {
        var buffer = new byte[2000];
        int len;
        while ((len = input.Read(buffer, 0, 2000)) > 0)
        {
            output.Write(buffer, 0, len);
        }
        output.Flush();
    }
}