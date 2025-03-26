using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MockNetPcf.Api.Controllers;
using MockNetPcf.Api.Models;
using MockNetPcf.Api.Services;
using Moq;
using Xunit;

namespace MockNetPcf.Tests.Controllers
{
    public class MockControllerTests
    {
        private readonly Mock<ILogger<MockController>> _loggerMock;
        private readonly Mock<IMockService> _mockServiceMock;
        private readonly MockController _controller;

        public MockControllerTests()
        {
            _loggerMock = new Mock<ILogger<MockController>>();
            _mockServiceMock = new Mock<IMockService>();
            _controller = new MockController(_loggerMock.Object, _mockServiceMock.Object);
        }

        [Fact]
        public async Task StartServer_WhenSuccessful_ReturnsOkResult()
        {
            // Arrange
            _mockServiceMock.Setup(x => x.StartServerAsync()).ReturnsAsync(true);

            // Act
            var result = await _controller.StartServer();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<dynamic>(okResult.Value);
            Assert.Equal("Mock server started successfully", response.message);
        }

        [Fact]
        public async Task StartServer_WhenFailed_ReturnsBadRequest()
        {
            // Arrange
            _mockServiceMock.Setup(x => x.StartServerAsync()).ReturnsAsync(false);

            // Act
            var result = await _controller.StartServer();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<dynamic>(badRequestResult.Value);
            Assert.Equal("Failed to start mock server", response.message);
        }

        [Fact]
        public async Task StopServer_WhenSuccessful_ReturnsOkResult()
        {
            // Arrange
            _mockServiceMock.Setup(x => x.StopServerAsync()).ReturnsAsync(true);

            // Act
            var result = await _controller.StopServer();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<dynamic>(okResult.Value);
            Assert.Equal("Mock server stopped successfully", response.message);
        }

        [Fact]
        public async Task GetStatus_ReturnsCorrectStatus()
        {
            // Arrange
            _mockServiceMock.Setup(x => x.IsServerRunning()).Returns(true);

            // Act
            var result = _controller.GetStatus();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<dynamic>(okResult.Value);
            Assert.True(response.isRunning);
        }

        [Fact]
        public async Task AddMock_WhenSuccessful_ReturnsOkResult()
        {
            // Arrange
            var mockDefinition = new MockDefinition
            {
                Path = "/api/test",
                Method = "GET",
                StatusCode = 200,
                ResponseBody = "{\"message\":\"Success\"}"
            };
            _mockServiceMock.Setup(x => x.AddMockAsync(It.IsAny<MockDefinition>())).ReturnsAsync(true);

            // Act
            var result = await _controller.AddMock(mockDefinition);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<dynamic>(okResult.Value);
            Assert.Equal("Mock added successfully", response.message);
        }

        [Fact]
        public async Task AddMock_WithBadRequest_Returns400()
        {
            // Arrange
            var mockDefinition = new MockDefinition
            {
                Path = "/api/test",
                Method = "GET",
                StatusCode = 400,
                ErrorMessage = "Bad Request",
                ResponseBody = "{\"error\":\"Invalid request parameters\"}"
            };
            _mockServiceMock.Setup(x => x.AddMockAsync(It.IsAny<MockDefinition>())).ReturnsAsync(true);

            // Act
            var result = await _controller.AddMock(mockDefinition);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task AddMock_WithServerError_Returns500()
        {
            // Arrange
            var mockDefinition = new MockDefinition
            {
                Path = "/api/test",
                Method = "GET",
                StatusCode = 500,
                ErrorMessage = "Internal Server Error",
                ResponseBody = "{\"error\":\"An unexpected error occurred\"}"
            };
            _mockServiceMock.Setup(x => x.AddMockAsync(It.IsAny<MockDefinition>())).ReturnsAsync(true);

            // Act
            var result = await _controller.AddMock(mockDefinition);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetAllMocks_ReturnsListOfMocks()
        {
            // Arrange
            var mocks = new List<MockDefinition>
            {
                new MockDefinition { Path = "/api/test1", Method = "GET" },
                new MockDefinition { Path = "/api/test2", Method = "POST" }
            };
            _mockServiceMock.Setup(x => x.GetAllMocksAsync()).ReturnsAsync(mocks);

            // Act
            var result = await _controller.GetAllMocks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedMocks = Assert.IsType<List<MockDefinition>>(okResult.Value);
            Assert.Equal(2, returnedMocks.Count);
        }
    }
}