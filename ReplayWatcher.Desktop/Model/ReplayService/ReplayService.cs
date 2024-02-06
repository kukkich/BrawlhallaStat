using System.IO;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ReplayWatcher.Desktop.Model.ReplayService;

public class ReplayService : IReplayService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<ReplayService> _logger;

    private const string ApiClientName = "GeneralApiClient";

    public ReplayService(
        IHttpClientFactory httpClientFactory,
        ILogger<ReplayService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<UploadReplayResult> Upload(string filePath)
    {
        var httpClient = _httpClientFactory.CreateClient(ApiClientName);

        await using var fileStream = File.OpenRead(filePath);
        var fileContent = new StreamContent(fileStream);
        using var formData = new MultipartFormDataContent
        {
            { fileContent, "file", Path.GetFileName(filePath) }
        };

        var response = await httpClient.PostAsync("api/Replay/Upload", formData);

        if (response.IsSuccessStatusCode)
        {
            _logger.LogDebug("Replay uploaded successfully");

            return new UploadReplayResult(true, null);
        }

        _logger.LogWarning("Error while uploading replay");

        return new UploadReplayResult(false, new() { await response.Content.ReadAsStringAsync()});
    }
    
}