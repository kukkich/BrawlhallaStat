namespace BrawlhallaStat.Domain.Identity.Dto;

public class LoginResult
{
    public TokenPair TokenPair { get; set; } = null!;
    public AuthenticatedUser User { get; set; } = null!;
}