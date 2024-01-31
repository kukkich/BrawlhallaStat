using BrawlhallaStat.Domain;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Services;

public interface IBrawlhallaEntitiesService
{
    public Task<List<Legend>> GetLegends();
    public Task<List<Weapon>> GetWeapons();
}