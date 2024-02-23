namespace BrawlhallaStat.Domain.Dto;

public class StatisticDto
{
    public int Wins { get; set; }
    public int Lost { get; set; }
    public int Total => Wins + Lost;
}