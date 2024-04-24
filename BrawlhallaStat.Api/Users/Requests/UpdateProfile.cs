using BrawlhallaStat.Api.Contracts.Identity;
using BrawlhallaStat.Domain.Identity.Base;
using MediatR;

namespace BrawlhallaStat.Api.Users.Requests;

public record UpdateProfileRequest(IUserIdentity User, UpdateUserProfile NewProfile) : IRequest;