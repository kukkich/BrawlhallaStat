using BrawlhallaStat.Domain.Games.Dtos;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Services;

public interface IBrawlhallaEntitiesService
{
    public Task<List<LegendDto>> GetLegends();
    public Task<LegendDto> AddLegend();
    public Task<List<WeaponDto>> GetWeapons();
    public Task<LegendDto> AddWeapon();
}