using BrawlhallaStat.Domain.Base;
using BrawlhallaStat.Domain.Statistics;

namespace BrawlhallaStat.Domain;

public class User : IUserIdentity
{
    public string Id { get; set; } = null!;
    public string TelegramId { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string NickName => Login;

    public string TotalStatisticId { get; set; } = null!;
    public Statistic TotalStatistic { get; set; } = null!;

    public List<WeaponStatistic> WeaponStatistics { get; set; } = null!;
    public List<LegendStatistic> LegendStatistics { get; set; } = null!;
    public List<LegendAgainstLegendStatistic> LegendAgainstLegendStatistics { get; set; } = null!;
    public List<LegendAgainstWeaponStatistic> LegendAgainstWeaponStatistics { get; set; } = null!;
    public List<WeaponAgainstWeaponStatistic> WeaponAgainstWeaponStatistics { get; set; } = null!;
    public List<WeaponAgainstLegendStatistic> WeaponAgainstLegendStatistics { get; set; } = null!;

}