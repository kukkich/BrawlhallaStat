using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Identity.Dto;

namespace BrawlhallaStat.Domain.Identity;

public class AuthenticatedUser : IUserIdentity
{
    public string Id { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string NickName { get; set; } = null!;
    public string Email { get; set; } = null!;

    public List<RoleDto> Roles { get; set; } = null!;
    public List<ClaimDto> Claims { get; set; } = null!;

    IEnumerable<IRole> IUserIdentity.Roles => Roles;
    IEnumerable<IClaim> IUserIdentity.Claims => Claims;
}