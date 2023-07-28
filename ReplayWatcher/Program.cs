﻿using System.Diagnostics;

namespace ReplayWatcher;

internal class Program
{
    private const int DisplaySize = 20;
    private static FileSystemWatcher Watcher = null!;
    private static HttpClient HttpClient = null!;
    private static readonly string ApiUrl = "http://localhost:5190/api/replay/upload";

    private static async Task Main()
    {
        using var httpClient = new HttpClient();
        HttpClient = httpClient;

        Console.CursorVisible = false;
        var watcherCts = new CancellationTokenSource();
        var cts = new CancellationTokenSource();

        var token = cts.Token;
        var actions = new MenuAction[]
        {
            new("Start watching", () =>
            {
                string folderPath = "C:/Users/vitia/BrawlhallaReplays";
                var watcher = new FileSystemWatcher(folderPath);
                watcher.NotifyFilter = NotifyFilters.Attributes
                                       | NotifyFilters.CreationTime
                                       | NotifyFilters.DirectoryName
                                       | NotifyFilters.FileName
                                       | NotifyFilters.LastAccess
                                       | NotifyFilters.LastWrite
                                       | NotifyFilters.Security
                                       | NotifyFilters.Size;

                watcher.Changed += OnFileChanged;
                watcher.Created += OnFileCreated;
                

                //[7.10] SmallBrawlhaven (5).replay Created
                //11621
                //[7.10] SmallBrawlhaven (5).replay Changed
                //11621


                watcher.EnableRaisingEvents = true;
                Watcher = watcher;

                ReWriteLine("Наблюдение запущено");
            }),
            new("Stop watching", () =>
            {
                Watcher.Changed -= OnFileChanged;
                Watcher.Created -= OnFileCreated;

                ReWriteLine("Наблюдение остановлено");
            }),
            new("Выход", cts.Cancel),
        };


        var menu = new ConsoleMenu(actions);

        await menu.Run(token);
    }

    private static void OnFileCreated(object sender, FileSystemEventArgs e)
    {
        ReWriteLine($"{e.Name} {e.ChangeType}");
        if ((e.ChangeType & WatcherChangeTypes.Deleted) != 0)
        {
            Console.WriteLine($"{e.Name} {e.ChangeType}");
        }
        else
        {
            var bytes = File.ReadAllBytes(e.FullPath);

            var log = $"{e.Name} {e.ChangeType}\n" + bytes.Length;
            Console.WriteLine(log);
            UploadFile(e.FullPath);
        }

    }

    private static void OnFileChanged(object sender, FileSystemEventArgs e)
    {
        ReWriteLine($"{e.Name} {e.ChangeType}");
        if ((e.ChangeType & WatcherChangeTypes.Deleted) != 0)
        {
            Console.WriteLine($"{e.Name} {e.ChangeType}");
        }
        else
        {
            var bytes = File.ReadAllBytes(e.FullPath);

            var log = $"{e.Name} {e.ChangeType}\n" + bytes.Length;
            Console.WriteLine(log);
        }

    }

    private static async Task UploadFile(string filePath)
    {

        await using var fileStream = File.OpenRead(filePath);
        var fileContent = new StreamContent(fileStream);
        using var formData = new MultipartFormDataContent
        {
            { fileContent, "file", Path.GetFileName(filePath) }
        };

        Console.WriteLine("Отправляю запрос");

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var requestTask = HttpClient.PostAsync(ApiUrl, formData);
        var response = await requestTask;

        stopwatch.Stop();

        Console.WriteLine(response.IsSuccessStatusCode
            ? "Файл успешно загружен на сервер"
            : "Ошибка при загрузке файла на сервер");

        Console.WriteLine("Время выполнения запроса: " + stopwatch.Elapsed);
    }

    private static void ReWriteLine(string str)
    {
        ClearFirstRows(DisplaySize);
        var returnPosition = Console.GetCursorPosition();

        Console.WriteLine(str);

        Console.SetCursorPosition(returnPosition.Left, returnPosition.Top);
    }

    private static void ClearFirstRows(int rowsCount)
    {
        var returnPosition = Console.GetCursorPosition();

        for (int i = returnPosition.Top; i < rowsCount + returnPosition.Top; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write(new string(' ', Console.BufferWidth));
        }

        Console.SetCursorPosition(returnPosition.Left, returnPosition.Top);
    }
}