using System.Text;

namespace BrawlhallaStat.Api.Extensions;

public static class StringBuilderExtensions
{
    public static void RemoveLast(this StringBuilder sb, int count)
    {
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count), "count must be non negative");

        
        var startIndex = sb.Length - count;
        if (startIndex >= 0)
        {
            sb.Remove(startIndex, count);
        }
        else
        {
            throw new InvalidOperationException($"String builder has less than {count} length");
        }
    }
}