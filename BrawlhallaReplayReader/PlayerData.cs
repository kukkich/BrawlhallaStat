using BrawlhallaReplayReader;
using System.Collections.Generic;

namespace BrawlhallaReplayReader;


public class PlayerData
{
    public int ColorId { get; set; } = -1;
    public int SpawnBotId { get; set; } = -1;
    public int EmitterId { get; set; } = -1;
    public int PlayerThemeId { get; set; } = -1;
    public List<int> Taunts { get; set; } = new List<int>();
    public int WinTaunt { get; set; } = -1;
    public int LoseTaunt { get; set; } = -1;
    public List<int> OwnedTaunts { get; set; } = new List<int>();
    public int AvatarId { get; set; } = -1;
    public int Team { get; set; } = -1;
    public int Unknown2 { get; set; } = -1;
    public List<HeroData> Heroes { get; set; } = new List<HeroData>();
    public bool Bot { get; set; } = false;
    public bool HandicapsEnabled { get; set; } = false;
    public int HandicapStockCount { get; set; } = 3;
    public int HandicapDamageDoneMultiplier { get; set; } = 1;
    public int HandicapDamageTakenMultiplier { get; set; } = 1;

    public void Read(BitStream Stream, int heroCount)
    {
        ColorId = Stream.ReadInt();
        SpawnBotId = Stream.ReadInt();
        EmitterId = Stream.ReadInt();
        PlayerThemeId = Stream.ReadInt();

        Taunts = new List<int>();
        for (var i = 0; i < 8; i++)
        {
            Taunts.Add(Stream.ReadInt());
        }

        WinTaunt = Stream.ReadShort();
        LoseTaunt = Stream.ReadShort();

        OwnedTaunts = new List<int>();
        while (Stream.ReadBoolean())
        {
            OwnedTaunts.Add(Stream.ReadInt());
        }

        AvatarId = Stream.ReadShort();
        Team = Stream.ReadInt();
        Unknown2 = Stream.ReadInt();

        Heroes = new List<HeroData>();
        for (var i = 0; i < heroCount; i++)
        {
            Heroes.Add(HeroData.FromBitStream(Stream));
        }

        Bot = Stream.ReadBoolean();
        HandicapsEnabled = Stream.ReadBoolean();

        if (HandicapsEnabled)
        {
            HandicapStockCount = Stream.ReadInt();
            HandicapDamageDoneMultiplier = Stream.ReadInt();
            HandicapDamageTakenMultiplier = Stream.ReadInt();
        }
    }

    public void Write(BitStream Stream, int heroCount)
    {
        throw new NotImplementedException();
        Stream.WriteInt(ColorId);
        Stream.WriteInt(SpawnBotId);
        Stream.WriteInt(EmitterId);
        Stream.WriteInt(PlayerThemeId);

        if (Taunts.Count != 8)
        {
            throw new System.Exception("Invalid number of taunts");
        }
        for (var i = 0; i < 8; i++)
        {
            Stream.WriteInt(Taunts[i]);
        }

        //Stream.WriteShort(WinTaunt);
        //Stream.WriteShort(LoseTaunt);

        foreach (var ownedTaunt in OwnedTaunts)
        {
            Stream.WriteBoolean(true);
            Stream.WriteInt(ownedTaunt);
        }
        Stream.WriteBoolean(false);

        //Stream.WriteShort(AvatarId);
        Stream.WriteInt(Team);
        Stream.WriteInt(Unknown2);

        if (Heroes.Count != heroCount)
        {
            throw new System.Exception("Invalid number of heroes");
        }
        foreach (var hero in Heroes)
        {


            hero.Write(Stream);
        }

        Stream.WriteBoolean(Bot);
        Stream.WriteBoolean(HandicapsEnabled);

        if (HandicapsEnabled)
        {
            Stream.WriteInt(HandicapStockCount);
            Stream.WriteInt(HandicapDamageDoneMultiplier);
            Stream.WriteInt(HandicapDamageTakenMultiplier);
        }
    }

    public int CalcChecksum()
    {
        var checksum = 0;
        checksum += ColorId * 5;
        checksum += SpawnBotId * 93;
        checksum += EmitterId * 97;
        checksum += PlayerThemeId * 53;

        for (var i = 0; i < Taunts.Count; i++)
        {
            checksum += Taunts[i] * (13 + i);
        }

        checksum += WinTaunt * 37;
        checksum += LoseTaunt * 41;

        for (var i = 0; i < OwnedTaunts.Count; i++)
        {
            var taunt = OwnedTaunts[i];
            taunt -= (taunt >> 1) & 1431655765;
            taunt = (taunt & 858993459) + ((taunt >> 2) & 858993459);
            taunt = (((taunt + (taunt >> 4)) & 252645135) * 16843009) >> 24;

            checksum += taunt * (11 + i);
        }

        checksum += Team * 43;

        for (var i = 0; i < Heroes.Count; i++)
        {
            var hero = Heroes[i];

            checksum += hero.HeroId * (17 + i);
            checksum += hero.CostumeId * (7 + i);
            checksum += hero.Stance * (3 + i);
            checksum += hero.WeaponSkins * (2 + i);
        }

        if (!HandicapsEnabled)
        {
            checksum += 29;
        }
        else
        {
            checksum += HandicapStockCount * 31;
            checksum += (int)(System.Math.Round(HandicapDamageDoneMultiplier / 10.0) * 3);
            checksum += (int)(System.Math.Round(HandicapDamageTakenMultiplier / 10.0) * 23);
        }

        return checksum;
    }

    public static PlayerData FromBitStream(BitStream Stream, int heroCount)
    {
        var playerData = new PlayerData();
        playerData.Read(Stream, heroCount);
        return playerData;
    }
}