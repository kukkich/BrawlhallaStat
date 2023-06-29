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

        EnsureThatSupports(replay);
        var game = MapToDomainGame(replay);

        throw new NotImplementedException();

        //var replayData = ...

        //AddGameResultsToStatistics();

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

    private async Task AddGameResultsToStatistics(IUserIdentity user, Game game)
    {
        var userFromDb = await _dbContext.Users
            .Include(x => x.TotalStatistic)
            .Include(x => x.WeaponStatistics)
                .ThenInclude(x => x.Statistic)
            .Include(x => x.LegendStatistics)
                .ThenInclude(x => x.Statistic)
            .Include(x => x.LegendAgainstLegendStatistics)
                .ThenInclude(x => x.Statistic)
            .Include(x => x.LegendAgainstWeaponStatistics)
                .ThenInclude(x => x.Statistic)
            .Include(x => x.WeaponAgainstWeaponStatistics)
                .ThenInclude(x => x.Statistic)
            .Include(x => x.WeaponAgainstLegendStatistics)
                .ThenInclude(x => x.Statistic)
            .FirstAsync(x => x.Id == user.Id);

        var userFromGame = game.Players.FirstOrDefault(x => x.NickName == user.NickName);
        if (userFromGame is null)
        {
            throw new NoUserInGameException(user.NickName);
        }

        var legendId = userFromGame.LegendDetails.LegendId;
        var opponentLegendIds = game.Players
            .Where(x => x.Team != userFromGame.Team)
            .Select(x => x.LegendDetails.Legend.Id)
            .ToArray();

        var getLegendTask = _dbContext.Legends
            .FirstAsync(x => x.Id == legendId);

        if (userFromGame.IsWinner)
        {
            userFromDb.TotalStatistic.Wins++;

            userFromDb.LegendStatistics
                .First(x => x.LegendId == legendId)
                .Statistic
                .Wins++;

            foreach (var statistic in
                     userFromDb.LegendAgainstLegendStatistics
                         .Where(x => x.LegendId == legendId)
                         .Where(x => opponentLegendIds.Contains(x.OpponentLegendId))
                         .Select(x => x.Statistic)
                    )
            {
                statistic.Wins++;
            }

            var legend = await getLegendTask;
            foreach (var statistic in
                     userFromDb.WeaponStatistics
                         .Where(x => x.WeaponId == legend.FirstWeaponId ||
                                     x.WeaponId == legend.SecondWeaponId)
                         .Select(x => x.Statistic)
                    )
            {
                statistic.Wins++;
            }

            foreach (var statistic in
                     userFromDb.WeaponAgainstLegendStatistics
                         .Where(x => x.WeaponId == legend.FirstWeaponId ||
                                     x.WeaponId == legend.SecondWeaponId)
                         .Where(x => opponentLegendIds.Contains(x.OpponentLegendId))
                         .Select(x => x.Statistic)
                     )
            {
                statistic.Wins++;
            }

            var opponentWeaponIds = await (game.Type switch
                {
                    GameType.Ranked2V2 or GameType.Unranked2V2 => _dbContext.Legends
                        .Where(x => x.Id == opponentLegendIds[0] || x.Id == opponentLegendIds[1]),
                    GameType.Ranked1V1 or GameType.Unranked1V1 => _dbContext.Legends
                        .Where(x => x.Id == opponentLegendIds[0]),
                    _ => throw new NotSupportedException()
                })
                .SelectMany(x => new[] {x.FirstWeaponId, x.SecondWeaponId})
                .Distinct()
                .ToListAsync();

            foreach (var statistic in
                     userFromDb.LegendAgainstWeaponStatistics
                         .Where(x => x.LegendId == legendId)
                         .Where(x => opponentWeaponIds.Contains(x.OpponentWeaponId))
                         .Select(x => x.Statistic)
                    )
            {
                statistic.Wins++;
            }

            foreach (var statistic in
                     userFromDb.WeaponAgainstWeaponStatistics
                         .Where(x => x.WeaponId == legend.FirstWeaponId ||
                                     x.WeaponId == legend.SecondWeaponId)
                         .Where(x => opponentWeaponIds.Contains(x.OpponentWeaponId))
                         .Select(x => x.Statistic)
                    )
            {
                statistic.Wins++;
            }
        }
        else
        {
            userFromDb.TotalStatistic.Lost++;
        }


        throw new NotImplementedException();
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
                var domainPlayer = new Player
                {
                    NickName = player.Name,
                    Team = player.Data.Team switch
                    {
                        1 => Team.Red,
                        2 => Team.Blue,
                        _ => throw new Exception("Unexpected exception. Game has more than 2 teams")
                    },
                    IsWinner = replay.Results[1] == player.Data.Team,
                    Customization = new Customization(),
                    Game = game
                };

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

                var deaths =
                    from death in replay.Deaths
                    where death.EntityId == player.Id
                    select new Death
                    {
                        TimeStamp = death.Timestamp,
                        Player = domainPlayer,
                        Game = game
                    };

                domainPlayer.Customization = customization;
                domainPlayer.LegendDetails = legendDetails;
                domainPlayer.Deaths = deaths.ToList();

                return domainPlayer;
            })
            .ToList();
        game.Deaths = game.Players
            .SelectMany(x => x.Deaths)
            .OrderByDescending(x => x.TimeStamp)
            .ToList();

        game.Settings = new Domain.Game.GameSettings
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