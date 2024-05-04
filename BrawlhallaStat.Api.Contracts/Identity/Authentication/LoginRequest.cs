namespace BrawlhallaStat.Api.Contracts.Identity.Authentication;

public class LoginRequest
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}