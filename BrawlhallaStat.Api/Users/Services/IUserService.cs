using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Identity.Dto;

namespace BrawlhallaStat.Api.Users.Services;

public interface IUserService
{
    public Task UpdateProfile(IUserIdentity user, UpdateUserProfile newProfile);
}