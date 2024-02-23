namespace BrawlhallaStat.Domain.Identity.Authentication.Dto;

public class RegistrationModel
{
    public string Login { get; set; } = null!;
    public string NickName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
}