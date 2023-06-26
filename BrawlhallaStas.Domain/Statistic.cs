using BrawlhallaStat.Domain.Base;

namespace BrawlhallaStat.Domain;

public class Statistic : IHaveId<string>
{
    public string Id { get; set; } = null!;
    public int Wins { get; set; }
    public int Lost { get; set; }
    public int Total => Wins + Lost;
}