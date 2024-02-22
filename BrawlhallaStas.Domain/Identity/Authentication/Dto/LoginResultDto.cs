namespace BrawlhallaStat.Domain.Identity.Authentication.Dto;

public class LoginResultDto
{
    public string AccessToken { get; set; } = null!;
    public AuthenticatedUser User { get; set; } = null!;
}