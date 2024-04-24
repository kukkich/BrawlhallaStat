using BrawlhallaStat.Domain.Identity.Base;

namespace BrawlhallaStat.Api.Contracts.Identity.Authentication;

public class AuthenticatedUser : IUserIdentity
{
    public string Id { get; init; } = null!;
    public string Login { get; init; } = null!;
    public string NickName { get; init; } = null!;
    public string Email { get; init; } = null!;

    public List<RoleDto> Roles { get; init; } = null!;
    public List<ClaimDto> Claims { get; init; } = null!;

    IEnumerable<IRole> IUserIdentity.Roles => Roles;
    IEnumerable<IClaim> IUserIdentity.Claims => Claims;
}