using AutoMapper;
using BrawlhallaStat.Domain.Identity.Dto;
using BrawlhallaStat.Domain.Identity;
using BrawlhallaStat.Domain;
using System.Security.Claims;
using ClaimTypes = BrawlhallaStat.Domain.Identity.ClaimTypes;

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

        CreateMap<ClaimsPrincipal, AuthenticatedUser>()
            .ConvertUsing<ClaimsPrincipalToUserConverter>();
    }
}



public class ClaimsPrincipalToUserConverter : ITypeConverter<ClaimsPrincipal, AuthenticatedUser>
{
    public AuthenticatedUser Convert(ClaimsPrincipal source, AuthenticatedUser destination, ResolutionContext context)
    {
        var result = new AuthenticatedUser
        {
            Id = source.FindFirstValue(ClaimTypes.Id)!,
            Login = source.FindFirstValue(ClaimTypes.Name)!,
            NickName = source.FindFirstValue(ClaimTypes.NickName)!,
            Email = source.FindFirstValue(ClaimTypes.Email)!
        };

        var roleClaims = source.FindAll(ClaimTypes.Role);
        result.Roles = roleClaims
            .Select(roleClaim => new RoleDto {Name = roleClaim.Value})
            .ToList();

        var otherClaims = source.Claims
            .Where(claim => claim.Type != ClaimTypes.Id &&
                            claim.Type != ClaimTypes.Name &&
                            claim.Type != ClaimTypes.Email &&
                            claim.Type != ClaimTypes.NickName &&
                            claim.Type != ClaimTypes.Role);
        result.Claims = otherClaims
            .Select(claim => new ClaimDto {Name = claim.Type, Value = claim.Value})
            .ToList();


        return result;
    }
}