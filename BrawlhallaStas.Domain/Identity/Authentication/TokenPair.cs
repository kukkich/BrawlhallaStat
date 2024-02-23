namespace BrawlhallaStat.Domain.Identity.Authentication;

public class TokenPair
{
    public string Access { get; set; } = null!;
    public string Refresh { get; set; } = null!;
}