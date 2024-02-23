using System.Security.Claims;
using AutoMapper;
using BrawlhallaStat.Domain.Identity;
using BrawlhallaStat.Domain.Identity.Authentication;
using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Identity.Dto;
using ClaimTypes = BrawlhallaStat.Domain.Identity.ClaimTypes;

namespace BrawlhallaStat.Api.Authentication.MapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<IUserIdentity, List<Claim>>()
            .ConstructUsing(src => MapToClaims(src));

        CreateMap<ClaimsPrincipal, AuthenticatedUser>()
            .ConvertUsing<ClaimsPrincipalToUserConverter>();

        CreateMap<User, AuthenticatedUser>();
    }

    private List<Claim> MapToClaims(IUserIdentity userIdentity)
    {
        var claimList = new List<Claim>
        {
            new (ClaimTypes.Id, userIdentity.Id),
            new (ClaimTypes.Login, userIdentity.Login),
            new (ClaimTypes.Email, userIdentity.Email),
            new (ClaimTypes.NickName, userIdentity.NickName),
        };
        claimList.AddRange(userIdentity.Roles.Select(role =>
            new Claim(ClaimTypes.Role, role.Name))
        );
        claimList.AddRange(userIdentity.Claims.Select(claim =>
            new Claim(claim.Name, claim.Value))
        );
        return claimList;
    }
}

public class ClaimsPrincipalToUserConverter : ITypeConverter<ClaimsPrincipal, AuthenticatedUser>
{
    public AuthenticatedUser Convert(ClaimsPrincipal source, AuthenticatedUser destination, ResolutionContext context)
    {
        var roleClaims = source.FindAll(ClaimTypes.Role);
        var otherClaims = source.Claims
            .Where(claim => claim.Type != ClaimTypes.Id &&
                            claim.Type != ClaimTypes.Login &&
                            claim.Type != ClaimTypes.Email &&
                            claim.Type != ClaimTypes.NickName &&
                            claim.Type != ClaimTypes.Role);

        var result = new AuthenticatedUser
        {
            Id = source.FindFirstValue(ClaimTypes.Id)!,
            Login = source.FindFirstValue(ClaimTypes.Login)!,
            NickName = source.FindFirstValue(ClaimTypes.NickName)!,
            Email = source.FindFirstValue(ClaimTypes.Email)!,
            Roles = roleClaims
                .Select(roleClaim => new RoleDto { Name = roleClaim.Value })
                .ToList(),
            Claims = otherClaims
                .Select(claim => new ClaimDto { Name = claim.Type, Value = claim.Value })
                .ToList()
        };

        return result;
    }
}