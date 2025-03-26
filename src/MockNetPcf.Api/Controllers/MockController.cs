using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MockNetPcf.Api.Models;
using MockNetPcf.Api.Services;

namespace MockNetPcf.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MockController : ControllerBase
    {
        private readonly ILogger<MockController> _logger;
        private readonly IMockService _mockService;

        public MockController(ILogger<MockController> logger, IMockService mockService)
        {
            _logger = logger;
            _mockService = mockService;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartServer()
        {
            var result = await _mockService.StartServerAsync();
            if (result)
            {
                return Ok(new { message = "Mock server started successfully" });
            }
            return BadRequest(new { message = "Failed to start mock server" });
        }

        [HttpPost("stop")]
        public async Task<IActionResult> StopServer()
        {
            var result = await _mockService.StopServerAsync();
            if (result)
            {
                return Ok(new { message = "Mock server stopped successfully" });
            }
            return BadRequest(new { message = "Failed to stop mock server" });
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok(new { isRunning = _mockService.IsServerRunning() });
        }

        [HttpPost]
        public async Task<IActionResult> AddMock([FromBody] MockDefinition mockDefinition)
        {
            var result = await _mockService.AddMockAsync(mockDefinition);
            if (result)
            {
                return Ok(new { message = "Mock added successfully" });
            }
            return BadRequest(new { message = "Failed to add mock" });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MockDefinition>>> GetAllMocks()
        {
            var mocks = await _mockService.GetAllMocksAsync();
            return Ok(mocks);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveMock(string id)
        {
            var result = await _mockService.RemoveMockAsync(id);
            if (result)
            {
                return Ok(new { message = "Mock removed successfully" });
            }
            return BadRequest(new { message = "Failed to remove mock" });
        }

        [HttpPost("reset")]
        public async Task<IActionResult> ResetMocks()
        {
            var result = await _mockService.ResetMocksAsync();
            if (result)
            {
                return Ok(new { message = "Mocks reset successfully" });
            }
            return BadRequest(new { message = "Failed to reset mocks" });
        }
    }
}