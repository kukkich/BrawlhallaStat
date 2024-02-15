using BrawlhallaStat.Domain.Identity.Base;
using MediatR;

namespace BrawlhallaStat.Api.Users.Requests;

public record ChangeNickNameRequest(IUserIdentity User, string NewNickName) : IRequest;