using BrawlhallaStat.Api.Commands;
using BrawlhallaStat.Api.Exceptions.ReplayHandling;
using BrawlhallaStat.Domain;
using BrawlhallaStat.Domain.Context;
using MediatR;
using BrawlhallaReplayReader.Deserializers;
using BrawlhallaReplayReader.Models;
using BrawlhallaStat.Domain.Base;
using BrawlhallaStat.Domain.Game;
using Microsoft.EntityFrameworkCore;
using Player = BrawlhallaStat.Domain.Game.Player;

namespace BrawlhallaStat.Api.CommandHandlers;

public class UploadReplayHandler : IRequestHandler<UploadReplay, string>
{
    private readonly BrawlhallaStatContext _dbContext;
    private readonly IBHReplayDeserializer _replayDeserializer;
    private static readonly string[] AllowedPlaylistNames = { "2v2Ranked", "2v2Unranked", "1v1Ranked", "1v1Unranked" };
    private static readonly int[] AllowedResultKeys1V1 = { 1, 2 };
    private static readonly int[] AllowedResultKeys2V2 = { 1, 2, 3, 4 };
    private static readonly int[] AllowedResultValues1V1 = { 1, 2 };
    private static readonly int[] AllowedResultValues2V2 = { 1, 1, 2, 2 };

    public UploadReplayHandler(BrawlhallaStatContext dbContext, IBHReplayDeserializer replayDeserializer)
    {
        _dbContext = dbContext;
        _replayDeserializer = replayDeserializer;
    }

    public async Task<string> Handle(UploadReplay request, CancellationToken cancellationToken)
    {
        var file = request.File;
        ValidateFile(file);

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream, cancellationToken);

        var fileBytes = stream.ToArray();
        var replay = await _replayDeserializer.DeserializeAsync(fileBytes);

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

    private static void ValidateFile(IFormFile file)
    {
        if (file.Length > 100 * 1024) // (100 КБ)
        {
            throw new InvalidReplaySizeException();
        }

        var fileExtension = Path.GetExtension(file.FileName);
        if (fileExtension != ".replay")
        {
            throw new InvalidReplayFormatException();
        }
    }
    private static void EnsureThatSupports(ReplayInfo replay)
    {
        var name = replay.PlaylistName;
        if (AllowedPlaylistNames.Any(name.Contains))
        {
            throw new NotSupportedException("Not supported game type");
        }
        if (!replay.Players.All(player => player.Data.Team is 1 or 2))
        {
            throw new NotSupportedException("More than 2 team games not supported yet");
        }

        int playersExpectedNumber;
        if (name.Contains("2v2Ranked") || name.Contains("2v2Unranked"))
        {
            playersExpectedNumber = 4;
        }
        else
        {
            playersExpectedNumber = 2;
        }

        if (replay.Players.Count != playersExpectedNumber || replay.Results.Count != playersExpectedNumber)
        {
            throw new Exception($"Game has unexpected players number. {playersExpectedNumber} expected but was {replay.Players.Count}");
        }
        if (playersExpectedNumber == 2)
        {
            if (!replay.Results.Keys.SequenceEqual(AllowedResultKeys1V1) ||
                !replay.Results.Values.OrderBy(x => x).SequenceEqual(AllowedResultValues1V1))
            {
                throw new Exception($"Invalid results format");
            }
        }
        else
        {
            if (replay.Results.Keys.SequenceEqual(AllowedResultKeys2V2) ||
                !replay.Results.Values.OrderBy(x => x).SequenceEqual(AllowedResultValues2V2))
            {
                throw new Exception($"Invalid results format");
            }

            if (!(replay.Results[1] == replay.Results[2] &&
                  replay.Results[3] == replay.Results[4]))
            {
                throw new Exception($"Invalid results format");
            }
        }
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

    private Game MapToDomainGame(ReplayInfo replay)
    {
        var game = new Game();

        game.Id = Guid.NewGuid().ToString();

        game.RandomSeed = replay.RandomSeed;
        game.Version = replay.Version;
        game.OnlineGame = replay.OnlineGame;
        game.LevelId = replay.LevelId;
        game.EndOfMatchFanfare = replay.EndOfMatchFanfare;
        game.PlaylistName = replay.PlaylistName;
        game.Players = replay.Players.Select(player =>
        {
            var domainPlayer = new Player();
            domainPlayer.NickName = player.Name;
            domainPlayer.Team = player.Data.Team switch
            {
                1 => Team.Red,
                2 => Team.Blue,
                _ => throw new Exception("Unexpected exception. Game has more than 2 teams")
            };
            domainPlayer.Team
        });



    }
}