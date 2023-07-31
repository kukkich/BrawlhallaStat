using AutoMapper;
using BrawlhallaStat.Domain.Identity.Base;
using System.Security.Claims;

namespace BrawlhallaStat.Api.MapperProfiles;

public class UserIdentityProfile : Profile
{
    public UserIdentityProfile()
    {
        CreateMap<IUserIdentity, List<Claim>>()
            .ConstructUsing(src => MapClaims(src));
    }

    private List<Claim> MapClaims(IUserIdentity userIdentity)
    {
        var claimList = new List<Claim>
        {
            new (ClaimTypes.Name, userIdentity.Id),
            new (ClaimTypes.Email, userIdentity.Login),
            new (ClaimTypes.NameIdentifier, userIdentity.Email)
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