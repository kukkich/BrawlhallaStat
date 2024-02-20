using BrawlhallaStat.Domain.Identity.Base;

namespace BrawlhallaStat.Api.Users.Services;

public interface IUserService
{
    public Task ChangeNickName(IUserIdentity user, string newNickName);
}