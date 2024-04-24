using AutoMapper;
using BrawlhallaStat.Api.BrawlhallaEntities.Requests;
using BrawlhallaStat.Api.Contracts.GameEntities;
using BrawlhallaStat.Domain.GameEntities;

namespace BrawlhallaStat.Api.BrawlhallaEntities;

public class BrawlhallaEntitiesProfile : Profile
{
    public BrawlhallaEntitiesProfile()
    {
        CreateMap<AddLegendRequest, AddLegendDto>();
        CreateMap<AddLegendDto, Legend>();
        CreateMap<Legend, LegendDto>();

        CreateMap<AddWeaponRequest, Weapon>();
        CreateMap<Weapon, WeaponDto>().ReverseMap();
    }
}