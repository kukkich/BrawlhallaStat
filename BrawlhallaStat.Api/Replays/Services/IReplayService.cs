using BrawlhallaStat.Domain.Identity.Base;

namespace BrawlhallaStat.Api.Replays.Services;

public interface IReplayService
{
    public Task<string> Upload(IUserIdentity author, IFormFile file);
}