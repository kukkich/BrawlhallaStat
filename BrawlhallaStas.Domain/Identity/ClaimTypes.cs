namespace BrawlhallaStat.Domain.Identity;
using MicrosoftClaimTypes = System.Security.Claims.ClaimTypes;

public static class ClaimTypes
{
    public const string Id = MicrosoftClaimTypes.NameIdentifier;
    public const string NickName = "NickNameClaim";
    public const string Name = MicrosoftClaimTypes.Name;
    public const string Email = MicrosoftClaimTypes.Email;
    public const string Role = MicrosoftClaimTypes.Role;
}