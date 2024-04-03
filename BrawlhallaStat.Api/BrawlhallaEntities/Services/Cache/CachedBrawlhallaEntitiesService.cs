using BrawlhallaStat.Domain.GameEntities;
using BrawlhallaStat.Domain.GameEntities.Dtos;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Services.Cache;

public class CachedBrawlhallaEntitiesService : IBrawlhallaEntitiesService
{
    private readonly IBrawlhallaEntitiesService _originalService;

    public CachedBrawlhallaEntitiesService(IBrawlhallaEntitiesService originalService)
    {
        _originalService = originalService;
    }

    public Task<List<Legend>> GetLegends()
    {
        throw new NotImplementedException();
    }

    public Task<List<Weapon>> GetWeapons()
    {
        throw new NotImplementedException();
    }

    public Task AddWeapon(WeaponDto weapon)
    {
        return _originalService.AddWeapon(weapon);
    }

    public Task AddLegend(AddLegendDto legend)
    {
        return _originalService.AddLegend(legend);
    }
}