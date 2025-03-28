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
    /// <summary>
    /// Tests for the RecordingController class
    /// </summary>
    public class RecordingControllerTests
    {
        private readonly Mock<ILogger<RecordingController>> _loggerMock;
        private readonly Mock<IRecordingService> _recordingServiceMock;
        private readonly RecordingController _controller;

        public RecordingControllerTests()
        {
            _loggerMock = new Mock<ILogger<RecordingController>>();
            _recordingServiceMock = new Mock<IRecordingService>();
            _controller = new RecordingController(_loggerMock.Object, _recordingServiceMock.Object);
        }

        [Fact]
        public async Task StartRecording_WhenSuccessful_ReturnsOkResult()
        {
            // Arrange
            var options = new RecordingOptions
            {
                TargetUrl = "https://api.example.com",
                SaveMapping = true
            };
            _recordingServiceMock.Setup(x => x.StartRecordingAsync(It.IsAny<RecordingOptions>())).ReturnsAsync(true);

            // Act
            var result = await _controller.StartRecording(options);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            dynamic response = okResult.Value;
            Assert.Equal("Recording started successfully", response.message);
        }

        [Fact]
        public async Task StartRecording_WhenFailed_ReturnsBadRequest()
        {
            // Arrange
            var options = new RecordingOptions
            {
                TargetUrl = "https://api.example.com"
            };
            _recordingServiceMock.Setup(x => x.StartRecordingAsync(It.IsAny<RecordingOptions>())).ReturnsAsync(false);

            // Act
            var result = await _controller.StartRecording(options);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            dynamic response = badRequestResult.Value;
            Assert.Equal("Failed to start recording", response.message);
        }

        [Fact]
        public async Task StopRecording_WhenSuccessful_ReturnsOkResult()
        {
            // Arrange
            _recordingServiceMock.Setup(x => x.StopRecordingAsync()).ReturnsAsync(true);

            // Act
            var result = await _controller.StopRecording();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            dynamic response = okResult.Value;
            Assert.Equal("Recording stopped successfully", response.message);
        }

        [Fact]
        public async Task StopRecording_WhenFailed_ReturnsBadRequest()
        {
            // Arrange
            _recordingServiceMock.Setup(x => x.StopRecordingAsync()).ReturnsAsync(false);

            // Act
            var result = await _controller.StopRecording();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            dynamic response = badRequestResult.Value;
            Assert.Equal("Failed to stop recording", response.message);
        }

        [Fact]
        public void GetStatus_ReturnsCorrectStatus()
        {
            // Arrange
            _recordingServiceMock.Setup(x => x.IsRecording()).Returns(true);

            // Act
            var result = _controller.GetStatus();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            dynamic response = okResult.Value;
            Assert.True(response.isRecording);
        }
    }
}