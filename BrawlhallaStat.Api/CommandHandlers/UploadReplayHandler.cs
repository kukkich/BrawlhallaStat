using BrawlhallaStat.Api.Commands;
using BrawlhallaStat.Api.Exceptions.ReplayHandling;
using BrawlhallaStat.Domain;
using BrawlhallaStat.Domain.Context;
using MediatR;
using System.Text.RegularExpressions;
using BrawlhallaStat.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace BrawlhallaStat.Api.CommandHandlers;

public class UploadReplayHandler : IRequestHandler<UploadReplay, string>
{
    private readonly BrawlhallaStatContext _dbContext;

    public UploadReplayHandler(BrawlhallaStatContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> Handle(UploadReplay request, CancellationToken cancellationToken)
    {
        var file = request.File;
        
        if (file.Length > 100 * 1024) // (100 КБ)
        {
            throw new InvalidReplaySizeException();
        }
        var fileExtension = Path.GetExtension(file.FileName);
        if (fileExtension != ".replay")
        {
            throw new InvalidReplayFormatException();
        }

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream, cancellationToken);

        var fileData = stream.ToArray();
        throw new NotImplementedException();

        //var replayData = ...

        //AddMatchResultsToStatistics();

        throw new NotImplementedException();

        var fileModel = new ReplayFile
        {
            Id = Guid.NewGuid().ToString(),
            FileName = file.FileName,
            FileData = fileData
        };

        _dbContext.Replays.Add(fileModel);
        await _dbContext.SaveChangesAsync(cancellationToken);


        return fileModel.Id;
    }

    private async Task AddMatchResultsToStatistics(IUserIdentity user)
    {
        //TODO pass replayResult
        var userData = await _dbContext.Users
            .Include(x => x.TotalStatistic)
            .Include(x => x.WeaponStatistics)
            .Include(x => x.LegendStatistics)
            .Include(x => x.LegendAgainstLegendStatistics)
            .Include(x => x.LegendAgainstWeaponStatistics)
            .Include(x => x.WeaponAgainstWeaponStatistics)
            .Include(x => x.WeaponAgainstLegendStatistics)
            .FirstAsync(x => x.Id == user.Id);

        throw new NotImplementedException();

    }
}