using BrawlhallaStat.Domain.Identity.Authentication;
using BrawlhallaStat.Domain.Identity.Authentication.Dto;
using MediatR;

namespace BrawlhallaStat.Api.Authentication.Requests.Register;

public class RegisterUserRequest : RegistrationModel, IRequest<LoginResult>
    { }