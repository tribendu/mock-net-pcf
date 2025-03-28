using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MockNetPcf.Api.Models;
using MockNetPcf.Api.Services;

namespace MockNetPcf.Api.Controllers
{
    /// <summary>
    /// Controller for managing API recording functionality
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RecordingController : ControllerBase
    {
        private readonly ILogger<RecordingController> _logger;
        private readonly IRecordingService _recordingService;

        public RecordingController(ILogger<RecordingController> logger, IRecordingService recordingService)
        {
            _logger = logger;
            _recordingService = recordingService;
        }

        /// <summary>
        /// Starts recording API interactions from a target URL
        /// </summary>
        /// <param name="options">Recording configuration options</param>
        /// <returns>Status message indicating success or failure</returns>
        /// <response code="200">Recording started successfully</response>
        /// <response code="400">Failed to start recording or invalid options</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("start")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> StartRecording([FromBody] RecordingOptions options)
        {
            try
            {
                if (options == null || string.IsNullOrWhiteSpace(options.TargetUrl))
                {
                    _logger.LogWarning("Invalid recording options provided");
                    return BadRequest(new { message = "Target URL is required" });
                }

                var result = await _recordingService.StartRecordingAsync(options);
                if (result)
                {
                    _logger.LogInformation($"Recording started successfully for {options.TargetUrl}");
                    return Ok(new { message = "Recording started successfully", targetUrl = options.TargetUrl });
                }
                
                _logger.LogWarning($"Failed to start recording for {options.TargetUrl}");
                return BadRequest(new { message = "Failed to start recording" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting recording");
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An unexpected error occurred while starting recording", error = ex.Message });
            }
        }

        /// <summary>
        /// Stops the current recording session
        /// </summary>
        /// <returns>Status message indicating success or failure</returns>
        /// <response code="200">Recording stopped successfully</response>
        /// <response code="400">Failed to stop recording</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("stop")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> StopRecording()
        {
            try
            {
                if (!_recordingService.IsRecording())
                {
                    _logger.LogInformation("No active recording to stop");
                    return Ok(new { message = "No active recording to stop" });
                }

                var result = await _recordingService.StopRecordingAsync();
                if (result)
                {
                    _logger.LogInformation("Recording stopped successfully");
                    return Ok(new { message = "Recording stopped successfully" });
                }
                
                _logger.LogWarning("Failed to stop recording");
                return BadRequest(new { message = "Failed to stop recording" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error stopping recording");
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An unexpected error occurred while stopping recording", error = ex.Message });
            }
        }

        /// <summary>
        /// Gets the current recording status
        /// </summary>
        /// <returns>Status indicating if recording is active</returns>
        /// <response code="200">Returns recording status</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetStatus()
        {
            try
            {
                var isRecording = _recordingService.IsRecording();
                _logger.LogInformation($"Recording status checked: {(isRecording ? "active" : "inactive")}");
                return Ok(new { isRecording = isRecording });  
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking recording status");
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An unexpected error occurred while checking recording status", error = ex.Message });
            }
        }
    }
}