using BrawlhallaStat.Domain;
using BrawlhallaStat.Domain.Statistics;

namespace BrawlhallaStat.Api.Factories;

public interface IStatisticFactory
{
    public Task<List<WeaponAgainstWeaponStatistic>> CreateWeaponAgainstWeapon(string userId);
    public Task<List<WeaponAgainstLegendStatistic>> CreateWeaponAgainstLegend(string userId);
    public Task<List<LegendAgainstWeaponStatistic>> CreateLegendAgainstWeapon(string userId);
    public Task<List<LegendAgainstLegendStatistic>> CreateLegendAgainstLegend(string userId);
    public Task<List<WeaponStatistic>> CreateWeapon(string userId);
    public Task<List<LegendStatistic>> CreateLegend(string userId);
    public Statistic CreateSimple();
}