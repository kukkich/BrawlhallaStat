namespace BrawlhallaReplayReader;

public class BitStream
{
    private static readonly int[] Masks = {
        0, 1, 3, 7, 15, 31, 63, 127, 255, 511, 1023, 2047, 4095, 8191, 16383, 32767,
        65535, 131071, 262143, 524287, 1048575, 2097151, 4194303, 8388607, 16777215,
        33554431, 67108863, 134217727, 268435455, 536870911, 1073741823, 2147483647,
        -1,
    };

    private byte[] _data;

    public byte[] Data
    {
        get => _data;
        set => _data = value;
    }
    public int ReadOffset { get; set; }
    public int WriteOffset { get; set; }
    public int ReadBytesAvailable => (Data.Length * 8 - ReadOffset) / 8;
    public int WriteBytesAvailable => (Data.Length * 8 - WriteOffset) / 8;

    public BitStream(byte[]? bytes = null)
    {
        _data = bytes ?? Array.Empty<byte>();
        ReadOffset = 0;
        WriteOffset = 0;
    }

    public void Shrink()
    {
        Array.Resize(ref _data, (WriteOffset + 7) / 8);
    }

    public int ReadBits(int count)
    {
        var result = 0;

        while (count != 0)
        {
            if (ReadOffset >= Data.Length * 8)
                throw new EndOfStreamException();

            var byteIndex = ReadOffset / 8;
            var bitIndex = ReadOffset % 8;
            var remainingBits = 8 - bitIndex;
            var bitsToRead = count < remainingBits ? count : remainingBits;

            var mask = Masks[remainingBits];

            var value = (Data[byteIndex] & mask) >> (remainingBits - bitsToRead);
            result |= value << (count - bitsToRead);

            count -= bitsToRead;
            ReadOffset += bitsToRead;
        }

        return result;
    }

    public byte ReadByte()
    {
        return (byte)ReadBits(8);
    }

    public byte[] ReadByteList(int count)
    {
        var result = new byte[count];

        for (var i = 0; i < count; i++)
            result[i] = ReadByte();

        return result;
    }

    public char ReadChar()
    {
        return (char)ReadByte();
    }

    public short ReadShort()
    {
        return (short)ReadBits(16);
    }

    public int ReadInt()
    {
        return ReadBits(32);
    }

    public float ReadFloat()
    {
        var buffer = new byte[4];
        buffer[0] = (byte)ReadBits(8);
        buffer[1] = (byte)ReadBits(8);
        buffer[2] = (byte)ReadBits(8);
        buffer[3] = (byte)ReadBits(8);
        return BitConverter.ToSingle(buffer, 0);
    }

    public string ReadString()
    {
        var length = ReadShort();
        var buffer = ReadByteList(length);
        return System.Text.Encoding.UTF8.GetString(buffer);
    }

    public bool ReadBoolean()
    {
        return ReadBits(1) != 0;
    }

    public void WriteBits(int value, int count)
    {
        while (count > 0)
        {
            var byteOffset = WriteOffset / 8;
            var bitOffset = WriteOffset % 8;

            var bitsRemaining = 8 - bitOffset;

            var

                bitsToWrite = count < bitsRemaining ? count : bitsRemaining;

            var bits = (value & Masks[count]) >> (count - bitsToWrite);
            var wipeMask = Masks[count] >> (count - bitsToWrite);
            Data[byteOffset] &= (byte)~(wipeMask << (bitsRemaining - bitsToWrite));
            Data[byteOffset] |= (byte)(bits << (bitsRemaining - bitsToWrite));

            count -= bitsToWrite;
            WriteOffset += bitsToWrite;
        }
    }

    public void WriteByte(byte value)
    {
        WriteBits(value, 8);
    }

    public void WriteByteList(byte[] values)
    {
        foreach (var value in values)
            WriteByte(value);
    }

    public void WriteChar(char value)
    {
        WriteByte((byte)value);
    }

    public void WriteShort(short value)
    {
        WriteBits(value, 16);
    }

    public void WriteInt(int value)
    {
        WriteBits(value, 32);
    }

    public void WriteFloat(float value)
    {
        var buffer = BitConverter.GetBytes(value);
        WriteByteList(buffer);
    }

    public void WriteString(string value)
    {
        var buffer = System.Text.Encoding.UTF8.GetBytes(value);
        WriteShort((short)buffer.Length);
        WriteByteList(buffer);
    }

    public void WriteBoolean(bool value)
    {
        WriteBits(value ? 1 : 0, 1);
    }
}