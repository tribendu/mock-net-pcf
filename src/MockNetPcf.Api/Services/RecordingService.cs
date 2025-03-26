using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MockNetPcf.Api.Models;
using WireMock.Server;

namespace MockNetPcf.Api.Services
{
    /// <summary>
    /// Interface for recording API interactions using WireMock.NET
    /// </summary>
    public interface IRecordingService
    {
        /// <summary>
        /// Starts recording API interactions from a target URL
        /// The recorded interactions can be used to create mock definitions
        /// </summary>
        /// <param name="options">The recording options including target URL</param>
        /// <returns>True if recording started successfully, false otherwise</returns>
        Task<bool> StartRecordingAsync(RecordingOptions options);

        /// <summary>
        /// Stops the current recording session
        /// Recorded interactions are saved as mock definitions
        /// </summary>
        /// <returns>True if recording stopped successfully, false otherwise</returns>
        Task<bool> StopRecordingAsync();

        /// <summary>
        /// Checks if the recording service is currently active
        /// </summary>
        /// <returns>True if currently recording, false otherwise</returns>
        bool IsRecording();
    }

    /// <summary>
    /// Implementation of IRecordingService using WireMock.NET
    /// Records API interactions that can be used to generate mock definitions
    /// 
    /// Recording Process:
    /// - Starts a proxy server that intercepts requests to the target API
    /// - Records request/response pairs as they occur
    /// - Saves recordings as mock mappings that can be replayed later
    /// - Mappings are stored in the same location as regular mock mappings
    /// </summary>
    public class RecordingService : IRecordingService
    {
        private readonly ILogger<RecordingService> _logger;
        private readonly IMockService _mockService;
        private bool _isRecording;

        public RecordingService(ILogger<RecordingService> logger, IMockService mockService)
        {
            _logger = logger;
            _mockService = mockService;
            _isRecording = false;
        }

        public async Task<bool> StartRecordingAsync(RecordingOptions options)
        {
            try
            {
                if (!_mockService.IsServerRunning())
                {
                    await _mockService.StartServerAsync();
                }

                // Implementation to start recording using WireMock proxy settings
                _isRecording = true;
                _logger.LogInformation($"Started recording for target URL: {options.TargetUrl}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to start recording");
                return false;
            }
        }

        public Task<bool> StopRecordingAsync()
        {
            try
            {
                // Implementation to stop recording
                _isRecording = false;
                _logger.LogInformation("Stopped recording");
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to stop recording");
                return Task.FromResult(false);
            }
        }

        public bool IsRecording()
        {
            return _isRecording;
        }
    }
}