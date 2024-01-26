using BrawlhallaReplayReader.Deserializers.Binary;
using BrawlhallaReplayReader.Models;

namespace BrawlhallaReplayReader.Deserializers.ReadingStrategies;

internal class ReadingStrategyAfterV7dot13 : ReadingStrategyAfterV7
{
    public ReadingStrategyAfterV7dot13(BitStream stream)
        : base(stream)
    { }

    protected override void ReadGeneral(ReplayInfo destination)
    {
        destination.GameSettings = ReadGameSettings();

        // Probably the weapons and gadgets spawn configuration
        // See "Weapons & Gadgets selector in Custom Lobbies" 
        // At https://www.brawlhalla.com/news/patch-7-13-new-legend-loki
        // and https://brawlhalla.fandom.com/wiki/Patch_7.13
        var _ = Stream.ReadByteList(8);

        destination.LevelId = Stream.ReadInt();
        destination.HeroCount = Stream.ReadShort();
        destination.Players.Clear();
    }
}