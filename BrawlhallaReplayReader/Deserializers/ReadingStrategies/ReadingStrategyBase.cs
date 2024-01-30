using BrawlhallaReplayReader.Deserializers.Binary;
using BrawlhallaReplayReader.Models;

namespace BrawlhallaReplayReader.Deserializers.ReadingStrategies;

internal abstract class ReadingStrategyBase : IReadingStrategy
{
    protected BitStream Stream { get; set; }

    protected ReadingStrategyBase(BitStream stream)
    {
        Stream = stream;
    }

    public virtual ReplayInfo Read(ReplayInfo destination)
    {
        var stop = false;
        while (Stream.ReadBytesAvailable > 0 && !stop)
        {
            var state = ReadStateCode();

            switch (state)
            {
                case 1:
                    FillInputs(destination);
                    break;
                case 2:
                    stop = true;
                    break;
                case 3:
                    //ReadHeader(stream);
                    break;
                case 4:
                    ReadPlayerData(destination);
                    break;
                case 6:
                    FillResults(destination);
                    break;
                case 5:
                case 7:
                    FillFaces(destination, state == 5);
                    break;
                default:
                    throw new Exception("Unknown replayInfo read state: " + state);
            }
        }

        return destination;
    }

    protected virtual void ReadPlayerData(ReplayInfo destination)
    {
        ReadGeneral(destination);

        var calculatedChecksum = 0;

        while (Stream.ReadBoolean())
        {
            var entityId = Stream.ReadInt();
            var entityName = Stream.ReadString();

            var playerData = ReadPlayerData(destination.HeroCount);

            destination.Players.Add(new Player(
                entityId,
                entityName,
                playerData
            ));

            calculatedChecksum += GetPlayerDataChecksum(playerData);
        }

        var secondVersionCheck = Stream.ReadInt();

        if (secondVersionCheck != destination.Version)
        {
            throw new Exception("Second version check does not match first version check");
        }

        calculatedChecksum += destination.LevelId * 47;
        calculatedChecksum %= 173;

        var checksum = Stream.ReadInt();

        if (checksum != calculatedChecksum)
        {
            //TODO extract into logger
            Console.WriteLine($"[DEV WARNING] Data checksums don't match: Got {checksum}, calculated {calculatedChecksum}");
        }
    }

    protected virtual void ReadGeneral(ReplayInfo destination)
    {
        destination.GameSettings = ReadGameSettings();

        destination.LevelId = Stream.ReadInt();
        destination.HeroCount = Stream.ReadShort();
        destination.Players.Clear();
    }

    protected virtual GameSettings ReadGameSettings()
    {
        return new GameSettings
        {
            Flags = Stream.ReadInt(),
            MaxPlayers = Stream.ReadInt(),
            Duration = Stream.ReadInt(),
            RoundDuration = Stream.ReadInt(),
            StartingLives = Stream.ReadInt(),
            ScoringType = Stream.ReadInt(),
            ScoreToWin = Stream.ReadInt(),
            GameSpeed = Stream.ReadInt(),
            DamageRatio = Stream.ReadInt(),
            LevelSetId = Stream.ReadInt()
        };
    }

    protected virtual PlayerData ReadPlayerData(int heroCount)
    {
        var playerData = new PlayerData
        {
            ColorId = Stream.ReadInt(),
            SpawnBotId = Stream.ReadInt(),
            EmitterId = Stream.ReadInt(),
            PlayerThemeId = Stream.ReadInt(),
            Taunts = new List<int>()
        };

        for (var i = 0; i < 8; i++)
        {
            playerData.Taunts.Add(Stream.ReadInt());
        }

        playerData.WinTaunt = Stream.ReadShort();
        playerData.LoseTaunt = Stream.ReadShort();

        playerData.OwnedTaunts = new List<int>();
        while (Stream.ReadBoolean())
        {
            playerData.OwnedTaunts.Add(Stream.ReadInt());
        }

        playerData.AvatarId = Stream.ReadShort();
        playerData.Team = Stream.ReadInt();
        playerData.Unknown2 = Stream.ReadInt();

        playerData.Heroes = new List<HeroData>();
        for (var i = 0; i < heroCount; i++)
        {
            playerData.Heroes.Add(ReadHeroData());
        }

        playerData.Bot = Stream.ReadBoolean();
        playerData.HandicapsEnabled = Stream.ReadBoolean();


        if (!playerData.HandicapsEnabled) return playerData;

        playerData.HandicapStockCount = Stream.ReadInt();
        playerData.HandicapDamageDoneMultiplier = Stream.ReadInt();
        playerData.HandicapDamageTakenMultiplier = Stream.ReadInt();

        return playerData;
    }

    protected virtual HeroData ReadHeroData()
    {
        return new HeroData
        {
            HeroId = Stream.ReadInt(),
            CostumeId = Stream.ReadInt(),
            Stance = Stream.ReadInt(),
            WeaponSkins = Stream.ReadInt(),
        };
    }

    protected virtual int GetPlayerDataChecksum(PlayerData playerData)
    {
        var checksum = 0;
        checksum += playerData.ColorId * 5;
        checksum += playerData.SpawnBotId * 93;
        checksum += playerData.EmitterId * 97;
        checksum += playerData.PlayerThemeId * 53;

        checksum += playerData.Taunts.Select((t, i) => t * (13 + i)).Sum();

        checksum += playerData.WinTaunt * 37;
        checksum += playerData.LoseTaunt * 41;

        checksum += (
            from taunt in playerData.OwnedTaunts
            select taunt - ((taunt >> 1) & 1431655765)
            into taunt
            select (taunt & 858993459) + ((taunt >> 2) & 858993459)
                into taunt
            select (((taunt + (taunt >> 4)) & 252645135) * 16843009) >> 24
            )
            .Select((taunt, i) => taunt * (11 + i)).Sum();

        checksum += playerData.Team * 43;

        for (var i = 0; i < playerData.Heroes.Count; i++)
        {
            var hero = playerData.Heroes[i];

            checksum += hero.HeroId * (17 + i);
            checksum += hero.CostumeId * (7 + i);
            checksum += hero.Stance * (3 + i);
            checksum += hero.WeaponSkins * (2 + i);
        }

        if (!playerData.HandicapsEnabled)
        {
            checksum += 29;
        }
        else
        {
            checksum += playerData.HandicapStockCount * 31;
            checksum += (int)(Math.Round(playerData.HandicapDamageDoneMultiplier / 10.0) * 3);
            checksum += (int)(Math.Round(playerData.HandicapDamageTakenMultiplier / 10.0) * 23);
        }

        return checksum;
    }

    protected virtual void FillResults(ReplayInfo destination)
    {
        destination.Length = Stream.ReadInt();
        var thirdVersionCheck = Stream.ReadInt();

        if (thirdVersionCheck != destination.Version)
        {
            throw new Exception("Third version check does not match first version check");
        }

        if (!Stream.ReadBoolean()) return;

        destination.Results.Clear();

        while (Stream.ReadBoolean())
        {
            var entityId = Stream.ReadBits(5);
            int result = Stream.ReadShort();
            destination.Results[entityId] = result;
        }
    }

    protected virtual void FillInputs(ReplayInfo destination)
    {
        destination.Inputs.Clear();
        while (Stream.ReadBoolean())
        {
            var entityId = Stream.ReadBits(5);
            var inputCount = Stream.ReadInt();

            if (!destination.Inputs.ContainsKey(entityId))
            {
                destination.Inputs[entityId] = new List<Input>();
            }

            for (var i = 0; i < inputCount; i++)
            {
                var timestamp = Stream.ReadInt();
                var inputState = Stream.ReadBoolean() ? Stream.ReadBits(14) : 0;
                destination.Inputs[entityId].Add(new Input(
                    timestamp,
                    inputState
                ));
            }
        }
    }

    protected virtual void FillFaces(ReplayInfo destination, bool kos)
    {
        var arr = kos ? destination.Deaths : destination.VictoryFaces;

        while (Stream.ReadBoolean())
        {
            var entityId = Stream.ReadBits(5);
            var timestamp = Stream.ReadInt();

            arr.Add(new Face(entityId, timestamp));
        }

        arr.Sort((a, b) => a.Timestamp - b.Timestamp);
    }

    protected virtual int ReadStateCode() => Stream.ReadBits(3);
}