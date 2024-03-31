namespace BrawlhallaStat.Api.General.Time;

public class CurrentTimeProvider : ITimeProvider
{
    public DateTime GetTime()
    {
        return DateTime.Now;
    }
}