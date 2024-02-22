using AutoMapper;
using BrawlhallaStat.Domain.Identity.Authentication;
using BrawlhallaStat.Domain.Identity.Authentication.Dto;

namespace BrawlhallaStat.Api.Authentication.MapperProfiles;

public class DtosProfile : Profile
{
    public DtosProfile()
    {
        CreateMap<LoginResult, LoginResultDto>()
            .ForMember(x => x.AccessToken, opt => opt.MapFrom(src => src.TokenPair.Access));
    }
}