namespace BrawlhallaStat.Api.Contracts.Identity.Authentication;

public class LoginResultDto
{
    public string AccessToken { get; set; } = null!;
    public AuthenticatedUser User { get; set; } = null!;
}