using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MockNetPcf.Api.Models;
using MockNetPcf.Api.Services;

namespace MockNetPcf.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecordingController : ControllerBase
    {
        private readonly ILogger<RecordingController> _logger;
        private readonly IRecordingService _recordingService;

        public RecordingController(ILogger<RecordingController> logger, IRecordingService recordingService)
        {
            _logger = logger;
            _recordingService = recordingService;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartRecording([FromBody] RecordingOptions options)
        {
            var result = await _recordingService.StartRecordingAsync(options);
            if (result)
            {
                return Ok(new { message = "Recording started successfully" });
            }
            return BadRequest(new { message = "Failed to start recording" });
        }

        [HttpPost("stop")]
        public async Task<IActionResult> StopRecording()
        {
            var result = await _recordingService.StopRecordingAsync();
            if (result)
            {
                return Ok(new { message = "Recording stopped successfully" });
            }
            return BadRequest(new { message = "Failed to stop recording" });
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok(new { isRecording = _recordingService.IsRecording() });
        }
    }
}