namespace BrawlhallaStat.Domain.Identity.Dto;

public class UserLogin
{
    public string Login { get; } = null!;
    public string Password { get; set; } = null!;
}