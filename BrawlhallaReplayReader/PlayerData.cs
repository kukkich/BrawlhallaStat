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

    public void Read(BitStream data, int heroCount)
    {
        ColorId = data.ReadInt();
        SpawnBotId = data.ReadInt();
        EmitterId = data.ReadInt();
        PlayerThemeId = data.ReadInt();

        Taunts = new List<int>();
        for (var i = 0; i < 8; i++)
        {
            Taunts.Add(data.ReadInt());
        }

        WinTaunt = data.ReadShort();
        LoseTaunt = data.ReadShort();

        OwnedTaunts = new List<int>();
        while (data.ReadBoolean())
        {
            OwnedTaunts.Add(data.ReadInt());
        }

        AvatarId = data.ReadShort();
        Team = data.ReadInt();
        Unknown2 = data.ReadInt();

        Heroes = new List<HeroData>();
        for (var i = 0; i < heroCount; i++)
        {
            Heroes.Add(HeroData.FromBitStream(data));
        }

        Bot = data.ReadBoolean();
        HandicapsEnabled = data.ReadBoolean();

        if (HandicapsEnabled)
        {
            HandicapStockCount = data.ReadInt();
            HandicapDamageDoneMultiplier = data.ReadInt();
            HandicapDamageTakenMultiplier = data.ReadInt();
        }
    }

    public void Write(BitStream data, int heroCount)
    {
        throw new NotImplementedException();
        data.WriteInt(ColorId);
        data.WriteInt(SpawnBotId);
        data.WriteInt(EmitterId);
        data.WriteInt(PlayerThemeId);

        if (Taunts.Count != 8)
        {
            throw new System.Exception("Invalid number of taunts");
        }
        for (var i = 0; i < 8; i++)
        {
            data.WriteInt(Taunts[i]);
        }

        //data.WriteShort(WinTaunt);
        //data.WriteShort(LoseTaunt);

        foreach (var ownedTaunt in OwnedTaunts)
        {
            data.WriteBoolean(true);
            data.WriteInt(ownedTaunt);
        }
        data.WriteBoolean(false);

        //data.WriteShort(AvatarId);
        data.WriteInt(Team);
        data.WriteInt(Unknown2);

        if (Heroes.Count != heroCount)
        {
            throw new System.Exception("Invalid number of heroes");
        }
        foreach (var hero in Heroes)
        {


            hero.Write(data);
        }

        data.WriteBoolean(Bot);
        data.WriteBoolean(HandicapsEnabled);

        if (HandicapsEnabled)
        {
            data.WriteInt(HandicapStockCount);
            data.WriteInt(HandicapDamageDoneMultiplier);
            data.WriteInt(HandicapDamageTakenMultiplier);
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

    public static PlayerData FromBitStream(BitStream data, int heroCount)
    {
        var playerData = new PlayerData();
        playerData.Read(data, heroCount);
        return playerData;
    }
}