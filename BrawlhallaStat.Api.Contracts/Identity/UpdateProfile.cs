namespace BrawlhallaStat.Api.Contracts.Identity;

public class UpdateUserProfile
{
    public string NickName { get; set; } = null!;
    public string Email { get; set; } = null!;
}