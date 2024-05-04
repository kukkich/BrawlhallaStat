using AutoMapper;
using BrawlhallaStat.Api.Contracts.Statistics;
using BrawlhallaStat.Domain.Statistics;
using BrawlhallaStat.Domain.Statistics.Views;

namespace BrawlhallaStat.Api.Statistics.MapperProfiles;

public class FiltersProfile : Profile
{
    public FiltersProfile()
    {
        CreateMap<StatisticFilterCreateDto, StatisticFilter>();
        CreateMap<StatisticFilter, StatisticFilterPublicDto>();
        CreateMap<StatisticWithFilter, StatisticWithFilterDto>();

        CreateMap<FilterView, StatisticWithFilterDto>()
            .ForMember(dest => dest.Statistic, opt => opt.MapFrom(src => new Statistic
            {
                Wins = src.Wins,
                Defeats = src.Defeats
            }))
            .ForMember(dest => dest.Filter, opt => opt.MapFrom(src => new StatisticFilterPublicDto
            {
                Id = src.FilterId,
                CreatedAt = src.CreatedAt,
                GameType = src.GameType,
                LegendId = src.LegendId,
                WeaponId = src.WeaponId,
                EnemyLegendId = src.EnemyLegendId,
                EnemyWeaponId = src.EnemyWeaponId,
                TeammateLegendId = src.TeammateLegendId,
                TeammateWeaponId = src.TeammateWeaponId
            }));
    }
}