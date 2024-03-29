﻿using Microsoft.Extensions.Logging;

namespace ReplayWatcher.Desktop.Model.ReplayService;

public class LoggerReplayService : IReplayService
{
    private readonly ILogger<LoggerReplayService> _logger;

    public LoggerReplayService(ILogger<LoggerReplayService> logger)
    {
        _logger = logger;
    }

    public Task<UploadReplayResult> Upload(string filePath)
    {
        _logger.LogInformation("\"{filePath}\" uploaded", filePath);

        return Task.FromResult(new UploadReplayResult(false, null));
    }
}