namespace BrawlhallaStat.Domain.Identity.Dto;

public class UserRegistration
{
    public string Login { get; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}