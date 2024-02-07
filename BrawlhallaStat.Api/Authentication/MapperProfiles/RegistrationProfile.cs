using AutoMapper;
using BrawlhallaStat.Api.Authentication.Commands.Register;
using BrawlhallaStat.Api.Authentication.Services.Auth;

namespace BrawlhallaStat.Api.Authentication.MapperProfiles;

public class RegistrationProfile : Profile
{
    public RegistrationProfile()
    {
        CreateMap<RegisterUserCommand, RegistrationData>()
            .ReverseMap();
    }
}