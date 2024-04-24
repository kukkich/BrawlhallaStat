using BrawlhallaStat.Api.Contracts.Identity.Authentication;
using MediatR;

namespace BrawlhallaStat.Api.Authentication.Requests.Register;

public class RegisterUserRequest : RegistrationModel, IRequest<LoginResult>
    { }