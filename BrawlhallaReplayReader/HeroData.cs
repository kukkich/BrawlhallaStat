namespace BrawlhallaReplayReader;


public class HeroData
{
    public int HeroId { get; set; } = -1;
    public int CostumeId { get; set; } = -1;
    public int Stance { get; set; } = -1;
    public int WeaponSkins { get; set; } = -1;

    public void Read(BitStream data)
    {
        HeroId = data.ReadInt();
        CostumeId = data.ReadInt();
        Stance = data.ReadInt();
        WeaponSkins = data.ReadInt(); // split into one high field and one low field (16 bits each)
    }

    public void Write(BitStream data)
    {
        data.WriteInt(HeroId);
        data.WriteInt(CostumeId);
        data.WriteInt(Stance);
        data.WriteInt(WeaponSkins);
    }

    public static HeroData FromBitStream(BitStream data)
    {
        var heroData = new HeroData();
        heroData.Read(data);
        return heroData;
    }
}