using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Identity.Dto;
using MediatR;

namespace BrawlhallaStat.Api.Users.Requests;

public record UpdateProfileRequest(IUserIdentity User, UpdateUserProfile NewProfile) : IRequest;