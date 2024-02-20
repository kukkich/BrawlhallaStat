using BrawlhallaStat.Api.Caching;
using BrawlhallaStat.Api.Exceptions;
using BrawlhallaStat.Domain.GameEntities;
using BrawlhallaStat.Domain.GameEntities.Dtos;
using AutoMapper;
using BrawlhallaStat.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Services;

public class BrawlhallaEntitiesService : IBrawlhallaEntitiesService
{
    private readonly ICacheService<List<Legend>> _legendsCache;
    private readonly ICacheService<List<Weapon>> _weaponsCache;
    private readonly BrawlhallaStatContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<BrawlhallaEntitiesService> _logger;

    public BrawlhallaEntitiesService(
        ICacheService<List<Legend>> legendsCache,
        ICacheService<List<Weapon>> weaponsCache,
        BrawlhallaStatContext dbContext,
        IMapper mapper,
        ILogger<BrawlhallaEntitiesService> logger
    )
    {
        _legendsCache = legendsCache;
        _weaponsCache = weaponsCache;
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<Legend>> GetLegends()
    {
        var legends = await _legendsCache.GetDataAsync();
        return legends;
    }

    public async Task<List<Weapon>> GetWeapons()
    {
        var weapons = await _weaponsCache.GetDataAsync();
        return weapons;
    }

    public async Task AddWeapon(Weapon weapon)
    {
        var sameNameExist = await _dbContext.Weapons
            .AnyAsync(x => x.Name == weapon.Name);
        if (sameNameExist)
        {
            throw new AlreadyExistException(
            who: nameof(Weapon),
                propertyName: nameof(Weapon.Name),
                value: weapon.Name
            );
        }
        var sameIdExist = await _dbContext.Weapons
            .AnyAsync(x => x.Name == weapon.Name);
        if (sameIdExist)
        {
            throw new AlreadyExistException(
                who: nameof(Weapon),
                propertyName: nameof(Weapon.Id),
                value: weapon.Id.ToString()
            );
        }
        _dbContext.Weapons.Add(weapon);

        await _dbContext.SaveChangesAsync();
    }

    public async Task AddLegend(LegendDto legend)
    {
        var sameNameExist = await _dbContext.Legends
            .AnyAsync(x => x.Name == legend.Name);
        if (sameNameExist)
        {
            throw new AlreadyExistException(
                who: nameof(Legend),
                propertyName: nameof(Legend.Name),
                value: legend.Name
            );
        }
        var sameIdExist = await _dbContext.Legends
            .AnyAsync(x => x.Id == legend.Id);
        if (sameIdExist)
        {
            throw new AlreadyExistException(
                who: nameof(Legend),
                propertyName: nameof(Legend.Id),
                value: legend.Id.ToString()
            );
        }

        var newLegend = _mapper.Map<Legend>(legend);
        
        _dbContext.Legends.Add(newLegend);
        await _dbContext.SaveChangesAsync();
        
        _logger.LogInformation("Legend added, id {Id}", newLegend.Id);
    }
}