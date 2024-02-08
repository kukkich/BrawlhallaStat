using BrawlhallaStat.Domain.GameEntities;
using BrawlhallaStat.Domain.GameEntities.Dtos;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Services;

public interface IBrawlhallaEntitiesService
{
    public Task<List<Legend>> GetLegends();
    public Task<List<Weapon>> GetWeapons();
    public Task AddWeapon(Weapon weapon);
    public Task AddLegend(LegendDto legend);
}