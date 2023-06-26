using BrawlhallaStat.Domain.Base;
using BrawlhallaStat.Domain.Statistics;

namespace BrawlhallaStat.Domain;

public class User : IHaveId<string>
{
    public string Id { get; set; } = null!;
    public string TelegramId { get; set; } = null!;
    public string Login { get; set; } = null!;


    public string TotalStatisticId { get; set; } = null!;
    public Statistic TotalStatistic { get; set; } = null!;

    public WeaponStatistic[] WeaponStatistics { get; set; } = null!;
    public LegendStatistic[] LegendStatistics { get; set; } = null!;
    public LegendAgainstLegendStatistic[] LegendAgainstLegendStatistics { get; set; } = null!;
    public LegendAgainstWeaponStatistic[] LegendAgainstWeaponStatistics { get; set; } = null!;
    public WeaponAgainstWeaponStatistic[] WeaponAgainstWeaponStatistics { get; set; } = null!;
    public WeaponAgainstLegendStatistic[] WeaponAgainstLegendStatistics { get; set; } = null!;

}