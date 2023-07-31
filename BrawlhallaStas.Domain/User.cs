using BrawlhallaStat.Domain.Identity;
using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Statistics;

namespace BrawlhallaStat.Domain;

public class User : IUserIdentity
{
    public string Id { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string NickName { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    public string TotalStatisticId { get; set; } = null!;
    public Statistic TotalStatistic { get; set; } = null!;

    public List<WeaponStatistic> WeaponStatistics { get; set; } = null!;
    public List<LegendStatistic> LegendStatistics { get; set; } = null!;
    public List<LegendAgainstLegendStatistic> LegendAgainstLegendStatistics { get; set; } = null!;
    public List<LegendAgainstWeaponStatistic> LegendAgainstWeaponStatistics { get; set; } = null!;
    public List<WeaponAgainstWeaponStatistic> WeaponAgainstWeaponStatistics { get; set; } = null!;
    public List<WeaponAgainstLegendStatistic> WeaponAgainstLegendStatistics { get; set; } = null!;

    public List<Role> Roles { get; set; } = null!;
    public List<IdentityClaim> Claims { get; set; } = null!;

    IEnumerable<IRole> IUserIdentity.Roles => Roles;
    IEnumerable<IClaim> IUserIdentity.Claims => Claims;
}