using BrawlhallaStat.Api.Contracts.Identity.Authentication;
using MediatR;

namespace BrawlhallaStat.Api.Authentication.Requests.Login;

public class LoginUserRequest : LoginModel, IRequest<LoginResult>
    { }