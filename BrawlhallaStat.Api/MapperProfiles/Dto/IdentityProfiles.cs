using AutoMapper;
using BrawlhallaStat.Domain.Identity.Dto;
using BrawlhallaStat.Domain.Identity;
using BrawlhallaStat.Domain;

namespace BrawlhallaStat.Api.MapperProfiles.Dto;

public class IdentityProfiles : Profile
{
    public IdentityProfiles()
    {
        CreateMap<Role, RoleDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<IdentityClaim, ClaimDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

        CreateMap<User, AuthenticatedUser>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login))
            .ForMember(dest => dest.NickName, opt => opt.MapFrom(src => src.NickName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles))
            .ForMember(dest => dest.Claims, opt => opt.MapFrom(src => src.Claims));
    }
}