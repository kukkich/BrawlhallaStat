namespace BrawlhallaStat.Domain.Identity.Dto;

public class TokenPair
{
    public string Access { get; set; } = null!;
    public string Refresh { get; set; } = null!;
}