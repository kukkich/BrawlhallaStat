using BrawlhallaStat.Api.Exceptions;
using BrawlhallaStat.Domain.GameEntities;
using BrawlhallaStat.Domain.GameEntities.Dtos;
using AutoMapper;
using BrawlhallaStat.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Api.BrawlhallaEntities.Services;

public class BrawlhallaEntitiesService : IBrawlhallaEntitiesService
{
    private readonly BrawlhallaStatContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<BrawlhallaEntitiesService> _logger;

    public BrawlhallaEntitiesService(
        BrawlhallaStatContext dbContext,
        IMapper mapper,
        ILogger<BrawlhallaEntitiesService> logger
    )
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<Legend>> GetLegends()
    {
        return await _dbContext.Legends
            .Include(x => x.FirstWeapon)
            .Include(x => x.SecondWeapon)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Weapon>> GetWeapons()
    {
        return await _dbContext.Weapons
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddWeapon(WeaponDto weapon)
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

        var newWeapon = _mapper.Map<Weapon>(weapon);
        _dbContext.Weapons.Add(newWeapon);

        await _dbContext.SaveChangesAsync();
    }

    public async Task AddLegend(AddLegendDto legend)
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