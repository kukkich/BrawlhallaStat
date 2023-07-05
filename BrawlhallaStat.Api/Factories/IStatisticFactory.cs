using BrawlhallaStat.Domain;
using BrawlhallaStat.Domain.Statistics;

namespace BrawlhallaStat.Api.Factories;

public interface IStatisticFactory
{
    public Task<WeaponAgainstWeaponStatistic[]> CreateWeaponAgainstWeapon(string userId);
    public Task<WeaponAgainstLegendStatistic[]> CreateWeaponAgainstLegend(string userId);
    public Task<LegendAgainstWeaponStatistic[]> CreateLegendAgainstWeapon(string userId);
    public Task<LegendAgainstLegendStatistic[]> CreateLegendAgainstLegend(string userId);
    public Task<WeaponStatistic[]> CreateWeapon(string userId);
    public Task<LegendStatistic[]> CreateLegend(string userId);
    public Statistic CreateSimple();
}