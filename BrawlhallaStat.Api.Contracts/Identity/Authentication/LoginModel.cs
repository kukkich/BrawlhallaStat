namespace BrawlhallaStat.Api.Contracts.Identity.Authentication;

public class LoginModel
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}