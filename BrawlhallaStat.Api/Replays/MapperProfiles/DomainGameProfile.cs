using AutoMapper;
using BrawlhallaReplayReader.Models;
using BrawlhallaStat.Domain.GameEntities;
using Player = BrawlhallaStat.Domain.GameEntities.Player;
using GameSettings = BrawlhallaStat.Domain.GameEntities.GameSettings;
using ReaderPlayer = BrawlhallaReplayReader.Models.Player;
using ReaderGameSettings = BrawlhallaReplayReader.Models.GameSettings;

namespace BrawlhallaStat.Api.Replays.MapperProfiles;

public class DomainGameProfile : Profile
{
    public DomainGameProfile()
    {
        CreateMap<ReplayInfo, GameDetail>()
            .ForMember(dest => dest.RandomSeed, opt => opt.MapFrom(src => src.RandomSeed))
            .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
            .ForMember(dest => dest.OnlineGame, opt => opt.MapFrom(src => src.OnlineGame))
            .ForMember(dest => dest.LevelId, opt => opt.MapFrom(src => src.LevelId))
            .ForMember(dest => dest.EndOfMatchFanfareId, opt => opt.MapFrom(src => src.EndOfMatchFanfare))
            .ForMember(dest => dest.PlaylistName, opt => opt.MapFrom(src => src.PlaylistName))
            .ForMember(dest => dest.Settings, opt => opt.MapFrom(src => MapGameSettings(src.GameSettings)))
            .AfterMap((src, dest) =>
            {
                dest.Players = src.Players.Select(player => MapPlayer(player, src, dest)).ToList();
                dest.Deaths = dest.Players.SelectMany(x => x.Deaths).OrderByDescending(x => x.TimeStamp).ToList();
            });
    }

    private GameSettings MapGameSettings(ReaderGameSettings data)
    {
        return new GameSettings
        {
            Flags = data.Flags,
            MaxPlayers = data.MaxPlayers,
            Duration = data.Duration,
            RoundDuration = data.RoundDuration,
            StartingLives = data.StartingLives,
            ScoringType = data.ScoringType,
            ScoreToWin = data.ScoreToWin,
            GameSpeed = data.GameSpeed,
            DamageRatio = data.DamageRatio,
            LevelSetId = data.LevelSetId
        };
    }

    private Player MapPlayer(ReaderPlayer playerData, ReplayInfo replayInfo, GameDetail gameDetail)
    {
        var customization = new Customization
        {
            ColorId = playerData.Data.ColorId,
            ThemeId = playerData.Data.PlayerThemeId,
            WinTaunt = playerData.Data.WinTaunt,
            LoseTaunt = playerData.Data.LoseTaunt,
            AvatarId = playerData.Data.AvatarId
        };

        var hero = playerData.Data.Heroes[0];
        var legendDetails = new LegendDetails
        {
            LegendId = hero.HeroId,
            CostumeId = hero.CostumeId,
            Stance = hero.Stance,
            WeaponSkins = hero.WeaponSkins
        };

        var domainPlayer = new Player
        {
            NickName = playerData.Name,
            Team = MapTeam(playerData.Data.Team),
            IsWinner = replayInfo.Results[1] == playerData.Data.Team,
            Customization = customization,
            LegendDetails = legendDetails,
            GameDetail = gameDetail,
        };
        
        domainPlayer.Deaths = replayInfo.Deaths
            .Where(death => death.EntityId == playerData.InGameId)
            .Select(death => MapDeath(death, domainPlayer)).ToList();
        return domainPlayer;
    }

    private Team MapTeam(int teamId)
    {
        return teamId switch
        {
            1 => Team.Red,
            2 => Team.Blue,
            _ => throw new Exception("Unexpected exception. Game has more than 2 teams")
        };
    }

    private Death MapDeath(Face deathData, Player player)
    {
        return new Death
        {
            TimeStamp = deathData.Timestamp,
            Player = player
        };
    }
}