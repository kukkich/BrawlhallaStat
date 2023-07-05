// ReSharper disable StringLiteralTypo

using System.Text.Json;
using BrawlhallaReplayReader.Deserializers;

namespace SandBox;

internal class Program
{
    private static string LegendsExploring = "[7.09] CrystalTemple (6).replay";
    private static string ValidReplay = "[6.11] SmallGalvanPrime (24).replay";
    private static string ValidReplay2 = "[6.10] SmallGalvanPrime (31).replay";

    private static string[] InvalidReplay = {
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
    private static string[] CustomReplays = {
        "[7.09] SmallBrawlhaven (1).replay",
        "[7.09] SmallKingsPass.replay",
        "[7.09] SmallBrawlhaven (2).replay",
        "[7.09] SmallEnigma (3).replay",
        "[7.09] SmallBrawlhaven (3).replay"
    };
    private static string InvalidMeOnWuShang = "[7.09] ShorwindFishing.replay";
    private static int ValidOffsets = 32;

    static async Task Main()
    {
        await SendToServer();
        //var path = GetReplayPath(LegendsExploring);
        //var lastMatchPath = GetNewestFile("C:/Users/vitia/BrawlhallaReplays");
        //var replayBinary = File.ReadAllBytes(path);
        //var reader = new BHReplayDeserializer();
        //var replay = reader.Deserialize(replayBinary);
        ////var heroId = replay.Players.First(x => x.Name == "Nasral V Szhopu")
        ////.Data
        ////.Heroes[0]
        ////.HeroId;
        ////Console.WriteLine(lastMatchPath);
        ////Console.WriteLine($"Nasral V Szhopu: {heroId}");
        ////var bot = replay.Players.Where(x => x.Name != "Nasral V Szhopu").Select(x => x.Data.Heroes[0])
        ////.First();
        ////Console.WriteLine($"Bot: {bot.HeroId}");
        //string json = JsonSerializer.Serialize(replay, new JsonSerializerOptions
        //{
        //    WriteIndented = true
        //});
        //Console.WriteLine(json);
        ////Console.WriteLine(replay
        //    //.Players.First(x => x.Name == "Nasral V Szhopu")
        //    //.Data.Team);
        //Console.WriteLine("Hello, World!");
    }

    private static string GetReplayPath(string replayName)
    {
        return "C:/Users/vitia/BrawlhallaReplays" + $"/{replayName}";
    }

    public static string GetNewestFile(string folderPath)
    {
        var files = Directory.GetFiles(folderPath);

        var newestFile = files[0];
        var newestFileCreationTime = File.GetCreationTime(newestFile);

        for (var i = 1; i < files.Length; i++)
        {
            var currentFile = files[i];
            var currentFileCreationTime = File.GetCreationTime(currentFile);

            if (currentFileCreationTime <= newestFileCreationTime) continue;

            newestFile = currentFile;
            newestFileCreationTime = currentFileCreationTime;
        }

        return newestFile;
    }

    public static async Task SendToServer()
    {
        string filePath = GetReplayPath(InvalidReplay[0]);
        string apiUrl = "http://localhost:5190/api/replay/upload";

        using var httpClient = new HttpClient();
        await using var fileStream = File.OpenRead(filePath);
        var fileContent = new StreamContent(fileStream);
        using var formData = new MultipartFormDataContent
        {
            { fileContent, "file", Path.GetFileName(filePath) }
        };

        var response = await httpClient.PostAsync(apiUrl, formData);

        Console.WriteLine(response.IsSuccessStatusCode
            ? "Файл успешно загружен на сервер"
            : "Ошибка при загрузке файла на сервер");

        Console.ReadLine();
    }
}
