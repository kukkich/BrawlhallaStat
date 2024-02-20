using AutoMapper;
using BrawlhallaStat.Api.Authentication.Requests.Register;
using BrawlhallaStat.Api.Authentication.Services.Auth;

namespace BrawlhallaStat.Api.Authentication.MapperProfiles;

public class RegistrationProfile : Profile
{
    public RegistrationProfile()
    {
        CreateMap<RegisterUserRequest, RegistrationData>()
            .ReverseMap();
    }
}