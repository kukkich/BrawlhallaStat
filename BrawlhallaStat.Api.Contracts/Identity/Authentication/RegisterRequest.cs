namespace BrawlhallaStat.Api.Contracts.Identity.Authentication;

public class RegisterRequest
{
    public string Login { get; set; } = null!;
    public string NickName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
}