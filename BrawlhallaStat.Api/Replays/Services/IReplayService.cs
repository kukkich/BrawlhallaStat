using BrawlhallaStat.Domain.GameEntities;
using BrawlhallaStat.Domain.Identity.Base;

namespace BrawlhallaStat.Api.Replays.Services;

public interface IReplayService
{
    public Task<Game> Upload(IUserIdentity author, IFormFile file);
}