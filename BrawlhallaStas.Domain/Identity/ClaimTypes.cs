namespace BrawlhallaStat.Domain.Identity;

public class ClaimTypes
{
    public const string Id = System.Security.Claims.ClaimTypes.NameIdentifier;
    public const string Login = System.Security.Claims.ClaimTypes.Name;
    public const string Email = System.Security.Claims.ClaimTypes.Email;
    public const string Role = System.Security.Claims.ClaimTypes.Role;
    public const string NickName = "NickName";
}