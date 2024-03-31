using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Statistics;

namespace BrawlhallaStat.Domain.Identity;

public class User : IUserIdentity
{
    public string Id { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string NickName { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    public List<Role> Roles { get; set; } = new();
    public List<IdentityClaim> Claims { get; set; } = new();

    public List<StatisticFilter> StatisticFilters { get; set; } = new();

    IEnumerable<IRole> IUserIdentity.Roles => Roles;
    IEnumerable<IClaim> IUserIdentity.Claims => Claims;
}