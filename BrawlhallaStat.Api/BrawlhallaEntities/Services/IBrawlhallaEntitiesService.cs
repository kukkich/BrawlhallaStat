using BrawlhallaStat.Domain;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Services;

public interface IBrawlhallaEntitiesService
{
    public Task<List<Legend>> GetLegends();
    public Task<Legend> AddLegend();
    public Task<List<Weapon>> GetWeapons();
    public Task<Legend> AddWeapon();
}