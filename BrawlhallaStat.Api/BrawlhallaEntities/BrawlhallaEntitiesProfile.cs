using AutoMapper;
using BrawlhallaStat.Api.BrawlhallaEntities.Requests;
using BrawlhallaStat.Domain.GameEntities;
using BrawlhallaStat.Domain.GameEntities.Dtos;

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