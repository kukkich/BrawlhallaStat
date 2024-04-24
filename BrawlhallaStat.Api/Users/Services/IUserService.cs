using BrawlhallaStat.Api.Contracts.Identity;
using BrawlhallaStat.Domain.Identity.Base;

namespace BrawlhallaStat.Api.Users.Services;

public interface IUserService
{
    public Task UpdateProfile(IUserIdentity user, UpdateUserProfile newProfile);
}