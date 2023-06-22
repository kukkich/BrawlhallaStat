// ReSharper disable StringLiteralTypo

using System.Text.Json;
using BrawlhallaReplayReader;

namespace SandBox;

internal class Program
{
    private static string ValidReplay = "[6.11] SmallGalvanPrime (24).replay";
    private static string ValidReplay2 = "[6.10] SmallGalvanPrime (31).replay";

    private static string[] InvalidReplay = new[]{
        "[7.09] SmallEnigma (2).replay",
        "[7.09] FlorenceTerrace.replay",
        "[7.09] Apocalypse (2).replay",
        "[7.09] WesternAirTemple (1).replay",
        "[7.09] TheGreatHall (3).replay",
        "[7.09] DemonIsland.replay",
        "[7.09] SmallFabledCity (1).replay",
        "[7.09] MammothFortress (4).replay",
        "[7.09] SpiritRealm (2).replay",
    };
    private static string[] CustomReplays = new[]{
        "[7.09] SmallBrawlhaven (1).replay",
        "[7.09] SmallKingsPass.replay",
        "[7.09] SmallBrawlhaven (2).replay",
        "[7.09] SmallEnigma (3).replay",
        "[7.09] SmallBrawlhaven (3).replay"
    };
    private static string InvalidMeOnWuShang = "[7.09] ShorwindFishing.replay";
    private static int ValidOffsets = 32;

    static void Main(string[] args)
    {
        var path = GetReplayPath(InvalidMeOnWuShang);
        var replayBinary = File.ReadAllBytes(path);
        var replay = ReplayData.ReadReplay(replayBinary);
        string json = JsonSerializer.Serialize(replay, new JsonSerializerOptions()
        {
            WriteIndented = true
        });
        Console.WriteLine(json);

        Console.WriteLine("Hello, World!");
    }


    private static string GetReplayPath(string replayName)
    {
        return "C:/Users/vitia/BrawlhallaReplays" + $"/{replayName}";
    }
}
