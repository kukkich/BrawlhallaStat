using BrawlhallaStat.Api.Exceptions;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Identity;
using BrawlhallaStat.Domain.Identity.Base;
using BrawlhallaStat.Domain.Identity.Dto;
using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Api.Users.Services;

public class UserService : IUserService
{
    private readonly BrawlhallaStatContext _dbContext;
    private readonly ILogger<UserService> _logger;

    public UserService(
        BrawlhallaStatContext dbContext, 
        ILogger<UserService> logger
        )
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task UpdateProfile(IUserIdentity user, UpdateUserProfile newProfile)
    {
        var updated = await _dbContext.Users.Where(x => x.Id == user.Id)
            .ExecuteUpdateAsync(x => 
                x.SetProperty(u => u.NickName, newProfile.NickName)
                    .SetProperty(u => u.Email, newProfile.Email)
            );

        if (updated == 0)
        {
            throw new EntityNotFoundException<User, string>(user.Id);
        }
    }
}