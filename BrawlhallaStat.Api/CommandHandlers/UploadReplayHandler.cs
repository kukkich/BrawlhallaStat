using BrawlhallaReplayReader.Deserializers;
using BrawlhallaReplayReader.Models;
using BrawlhallaStat.Api.CommandHandlers.ReplayHandling;
using BrawlhallaStat.Api.Commands;
using BrawlhallaStat.Api.Exceptions.ReplayHandling;
using BrawlhallaStat.Domain;
using BrawlhallaStat.Domain.Context;
using BrawlhallaStat.Domain.Games;
using MediatR;
using GameSettings = BrawlhallaStat.Domain.Games.GameSettings;
using Player = BrawlhallaStat.Domain.Games.Player;

namespace BrawlhallaStat.Api.CommandHandlers;

public class UploadReplayHandler : IRequestHandler<UploadReplay, string>
{

    private static readonly string[] AllowedPlaylistNames = { "2v2Ranked", "2v2Unranked", "1v1Ranked", "1v1Unranked" };
    private static readonly int[] AllowedResultKeys1V1 = { 1, 2 };
    private static readonly int[] AllowedResultKeys2V2 = { 1, 2, 3, 4 };
    private static readonly int[] AllowedResultValues1V1 = { 1, 2 };
    private static readonly int[] AllowedResultValues2V2 = { 1, 1, 2, 2 };

    private readonly BrawlhallaStatContext _dbContext;
    private readonly IBHReplayDeserializer _replayDeserializer;
    private readonly ReplayHandlingPipeline _replayHandlingPipeline;
    private readonly ILogger<UploadReplayHandler> _logger;

    public UploadReplayHandler(
        BrawlhallaStatContext dbContext,
        IBHReplayDeserializer replayDeserializer,
        ReplayHandlingPipeline replayHandlingPipeline,
        ILogger<UploadReplayHandler> logger
        )
    {
        _dbContext = dbContext;
        _replayDeserializer = replayDeserializer;
        _replayHandlingPipeline = replayHandlingPipeline;
        _logger = logger;
    }

    public async Task<string> Handle(UploadReplay request, CancellationToken cancellationToken)
    {
        try
        {

            var file = request.File;
            ValidateFile(file);

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream, cancellationToken);

            var fileBytes = stream.ToArray();
            var replay = _replayDeserializer.Deserialize(fileBytes);

            EnsureThatSupports(replay);
            var game = MapToDomainGame(replay);

            await _replayHandlingPipeline.Invoke(request.User, game);

            var fileModel = new ReplayFile
            {
                Id = Guid.NewGuid().ToString(),
                AuthorId = request.User.Id,
                FileName = file.FileName,
                FileData = fileBytes,
            };

            _dbContext.ReplayFiles.Add(fileModel);
            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Replay was saved");

            return fileModel.Id;
        }
        // TODO добавить отдельную ветку ошибок обработки реплея и отлавливать их отдельно
        catch
        {
            _logger.LogWarning("Error processing the replay");
            throw;
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

        int playersExpectedNumber;
        if (name.Contains("2v2Ranked") || name.Contains("2v2Unranked"))
        {
            playersExpectedNumber = 4;
        }
        else
        {
            playersExpectedNumber = 2;
        }

        if (replay.Players.Count != playersExpectedNumber)
        {
            throw new NotSupportedGameException(
                $"Game has unexpected players number. {playersExpectedNumber} expected but was {replay.Players.Count}"
                );
        }
        if (replay.Results.Count != playersExpectedNumber)
        {
            throw new UnfinishedGameException();
        }
        if (playersExpectedNumber == 2)
        {
            if (!replay.Results.Keys.SequenceEqual(AllowedResultKeys1V1) ||
                !replay.Results.Values.OrderBy(x => x).SequenceEqual(AllowedResultValues1V1))
            {
                throw new NotSupportedGameException($"Invalid results format");
            }
        }
        else
        {
            if (!replay.Results.Keys.SequenceEqual(AllowedResultKeys2V2) ||
                !replay.Results.Values.OrderBy(x => x).SequenceEqual(AllowedResultValues2V2))
            {
                throw new NotSupportedGameException($"Invalid results format");
            }

            if (!(replay.Results[1] == replay.Results[2] &&
                  replay.Results[3] == replay.Results[4]))
            {
                throw new NotSupportedGameException($"Invalid results format");
            }
        }
    }

    private Game MapToDomainGame(ReplayInfo replay)
    {
        var game = new Game
        {
            Id = Guid.NewGuid().ToString(),
            RandomSeed = replay.RandomSeed,
            Version = replay.Version,
            OnlineGame = replay.OnlineGame,
            LevelId = replay.LevelId,
            EndOfMatchFanfare = replay.EndOfMatchFanfare,
            PlaylistName = replay.PlaylistName
        };

        game.Players = replay.Players.
            Select(player =>
            {
                var customization = new Customization
                {
                    ColorId = player.Data.ColorId,
                    ThemeId = player.Data.PlayerThemeId,
                    WinTaunt = player.Data.WinTaunt,
                    LoseTaunt = player.Data.LoseTaunt,
                    AvatarId = player.Data.AvatarId
                };

                var hero = player.Data.Heroes[0];
                var legendDetails = new LegendDetails
                {
                    LegendId = hero.HeroId,
                    CostumeId = hero.CostumeId,
                    Stance = hero.Stance,
                    WeaponSkins = hero.WeaponSkins
                };

                var domainPlayer = new Player
                {
                    NickName = player.Name,
                    Team = player.Data.Team switch
                    {
                        1 => Team.Red,
                        2 => Team.Blue,
                        _ => throw new Exception("Unexpected exception. Game has more than 2 teams")
                    },
                    IsWinner = replay.Results[1] == player.Data.Team, // TODO remember how it works
                    Customization = customization,
                    LegendDetails = legendDetails,
                    Game = game
                };

                var deaths =
                    from death in replay.Deaths
                    where death.EntityId == player.Id
                    select new Death
                    {
                        TimeStamp = death.Timestamp,
                        Player = domainPlayer
                    };

                domainPlayer.Deaths = deaths.ToList();

                return domainPlayer;
            })
            .ToList();

        game.Deaths = game.Players
            .SelectMany(x => x.Deaths)
            .OrderByDescending(x => x.TimeStamp)
            .ToList();

        game.Settings = new GameSettings
        {
            Flags = replay.GameSettings.Flags,
            MaxPlayers = replay.GameSettings.MaxPlayers,
            Duration = replay.GameSettings.Duration,
            RoundDuration = replay.GameSettings.RoundDuration,
            StartingLives = replay.GameSettings.StartingLives,
            ScoringType = replay.GameSettings.ScoringType,
            ScoreToWin = replay.GameSettings.ScoreToWin,
            GameSpeed = replay.GameSettings.GameSpeed,
            DamageRatio = replay.GameSettings.DamageRatio,
            LevelSetId = replay.GameSettings.LevelSetId
        };

        return game;
    }
}