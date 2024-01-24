using AutoMapper;
using BrawlhallaReplayReader.Deserializers;
using BrawlhallaReplayReader.Models;
using BrawlhallaStat.Api.Replays.Exceptions;
using BrawlhallaStat.Api.Replays.ReplayHandling;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Games;
using BrawlhallaStat.Domain.Identity.Base;
using Player = BrawlhallaStat.Domain.Games.Player;

namespace BrawlhallaStat.Api.Replays.Services;

public class ReplayService : IReplayService
{
    private static readonly string[] AllowedPlaylistNames = { "2v2Ranked", "2v2Unranked", "1v1Ranked", "1v1Unranked" };
    private static readonly int[] AllowedResultKeys1V1 = { 1, 2 };
    private static readonly int[] AllowedResultKeys2V2 = { 1, 2, 3, 4 };
    private static readonly int[] AllowedResultValues1V1 = { 1, 2 };
    private static readonly int[] AllowedResultValues2V2 = { 1, 1, 2, 2 };

    private readonly IReplayDeserializer _replayDeserializer;
    private readonly ReplayHandlingPipeline _replayHandlingPipeline;
    private readonly BrawlhallaStatContext _dbContext;
    private readonly IMapper _mapper;

    public ReplayService(
        IReplayDeserializer replayDeserializer,
        ReplayHandlingPipeline replayHandlingPipeline,
        BrawlhallaStatContext dbContext,
        IMapper mapper
    )
    {
        _replayDeserializer = replayDeserializer;
        _replayHandlingPipeline = replayHandlingPipeline;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<string> Upload(IUserIdentity author, IFormFile file)
    {
        ValidateFile(file);

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        var fileBytes = stream.ToArray();
        var replay = _replayDeserializer.Deserialize(fileBytes);

        EnsureThatSupports(replay);

        var game = _mapper.Map<GameDetail>(replay);
        game.Id = Guid.NewGuid().ToString();

        var nickName = author.NickName;
        var authorAsPlayer = GetAuthorFromGame(game, nickName);
        game.AuthorTeam = authorAsPlayer.Team;

        game.Type = ...;

        var fileModel = new ReplayFile
        {
            Id = Guid.NewGuid().ToString(),
            FileName = file.FileName,
            FileData = fileBytes,
        };

        _dbContext.ReplayFiles.Add(fileModel);
        await _dbContext.SaveChangesAsync();

        return fileModel.Id;
    }

    private static Player GetAuthorFromGame(GameDetail game, string nickName)
    {
        try
        {
            var userFromGame = game.Players
                .SingleOrDefault(x => x.NickName == nickName);
            if (userFromGame is null)
            {
                throw new NoUserInGameException(nickName);
            }
            return userFromGame;
        }
        catch (InvalidOperationException)
        {
            throw new MultiplePlayersWithAuthorNickName(nickName);
        }
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
        if (!AllowedPlaylistNames.Any(name.Contains))
        {
            throw new NotSupportedGameException("Not supported game type");
        }
        
        if (!replay.Players.All(player => player.Data.Team is 1 or 2))
        {
            throw new NotSupportedGameException("More than 2 team games not supported yet");
        }

        int playersExpected;
        if (name.Contains("2v2Ranked") || name.Contains("2v2Unranked"))
        {
            playersExpected = 4;
        }
        else
        {
            playersExpected = 2;
        }

        if (replay.Players.Count != playersExpected)
        {
            throw new NotSupportedGameException(
                $"Game has unexpected players number. {playersExpected} expected but was {replay.Players.Count}"
            );
        }
        if (replay.Results.Count != playersExpected)
        {
            throw new UnfinishedGameException();
        }
        if (playersExpected == 2)
        {
            if (!replay.Results.Keys.SequenceEqual(AllowedResultKeys1V1) ||
                !replay.Results.Values.OrderBy(x => x).SequenceEqual(AllowedResultValues1V1))
            {
                throw new NotSupportedGameException($"Invalid results");
            }
        }
        else
        {
            if (!replay.Results.Keys.SequenceEqual(AllowedResultKeys2V2) ||
                !replay.Results.Values.OrderBy(x => x).SequenceEqual(AllowedResultValues2V2))
            {
                throw new NotSupportedGameException($"Invalid results");
            }

            if (!(replay.Results[1] == replay.Results[2] &&
                  replay.Results[3] == replay.Results[4]))
            {
                throw new NotSupportedGameException($"Invalid results");
            }
        }
    }
}