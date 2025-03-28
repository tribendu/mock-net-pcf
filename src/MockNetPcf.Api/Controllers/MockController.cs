using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MockNetPcf.Api.Models;
using MockNetPcf.Api.Services;

namespace MockNetPcf.Api.Controllers
{
    /// <summary>
    /// Controller for managing mock API endpoints
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MockController : ControllerBase
    {
        private readonly ILogger<MockController> _logger;
        private readonly IMockService _mockService;

        public MockController(ILogger<MockController> logger, IMockService mockService)
        {
            _logger = logger;
            _mockService = mockService;
        }

        /// <summary>
        /// Starts the mock server
        /// </summary>
        /// <returns>Status message indicating success or failure</returns>
        /// <response code="200">Server started successfully</response>
        /// <response code="400">Failed to start server</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("start")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> StartServer()
        {
            try
            {
                var result = await _mockService.StartServerAsync();
                if (result)
                {
                    _logger.LogInformation("Mock server started successfully");
                    return Ok(new { message = "Mock server started successfully" });
                }
                
                _logger.LogWarning("Failed to start mock server");
                return BadRequest(new { message = "Failed to start mock server" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting mock server");
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An unexpected error occurred while starting the server", error = ex.Message });
            }
        }

        /// <summary>
        /// Stops the mock server
        /// </summary>
        /// <returns>Status message indicating success or failure</returns>
        /// <response code="200">Server stopped successfully</response>
        /// <response code="400">Failed to stop server</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("stop")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> StopServer()
        {
            try
            {
                var result = await _mockService.StopServerAsync();
                if (result)
                {
                    _logger.LogInformation("Mock server stopped successfully");
                    return Ok(new { message = "Mock server stopped successfully" });
                }
                
                _logger.LogWarning("Failed to stop mock server");
                return BadRequest(new { message = "Failed to stop mock server" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error stopping mock server");
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An unexpected error occurred while stopping the server", error = ex.Message });
            }
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