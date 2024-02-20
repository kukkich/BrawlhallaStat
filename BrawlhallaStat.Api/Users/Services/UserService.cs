using BrawlhallaStat.Api.Authentication.Exceptions;
using BrawlhallaStat.Api.Exceptions;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Identity;
using BrawlhallaStat.Domain.Identity.Base;
using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Api.Users.Services;

public class UserService : IUserService
{
    private readonly BrawlhallaStatContext _dbContext;
    private readonly ILogger<UserService> _logger;

    public UserService(BrawlhallaStatContext dbContext, ILogger<UserService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task ChangeNickName(IUserIdentity user, string newNickName)
    {
        var updated = await _dbContext.Users.Where(x => x.Id == user.Id)
            .ExecuteUpdateAsync(x => x.SetProperty(u => u.NickName, newNickName));

        if (updated == 0)
        {
            throw new EntityNotFoundException<User, string>(user.Id);
        }
    }
}