using BrawlhallaStat.Domain.Identity.Authentication;
using BrawlhallaStat.Domain.Identity.Authentication.Dto;
using MediatR;

namespace BrawlhallaStat.Api.Authentication.Requests.Login;

public class LoginUserRequest : LoginModel, IRequest<LoginResult>
    { }