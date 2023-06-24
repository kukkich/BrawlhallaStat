namespace BrawlhallaReplayReader;


public class HeroData
{
    public int HeroId { get; set; } = -1;
    public int CostumeId { get; set; } = -1;
    public int Stance { get; set; } = -1;
    public int WeaponSkins { get; set; } = -1;

    public void Read(BitStream Stream)
    {
        HeroId = Stream.ReadInt();
        CostumeId = Stream.ReadInt();
        Stance = Stream.ReadInt();
        WeaponSkins = Stream.ReadInt(); // split into one high field and one low field (16 bits each)
    }

    public void Write(BitStream Stream)
    {
        Stream.WriteInt(HeroId);
        Stream.WriteInt(CostumeId);
        Stream.WriteInt(Stance);
        Stream.WriteInt(WeaponSkins);
    }

    public static HeroData FromBitStream(BitStream Stream)
    {
        var heroData = new HeroData();
        heroData.Read(Stream);
        return heroData;
    }
}