using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MockNetPcf.Api.Models;
using MockNetPcf.Api.Services;
using Moq;
using Xunit;

namespace MockNetPcf.Tests.Services
{
    public class RecordingServiceTests
    {
        private readonly Mock<ILogger<RecordingService>> _loggerMock;
        private readonly Mock<IMockService> _mockServiceMock;
        private readonly RecordingService _recordingService;

        public RecordingServiceTests()
        {
            _loggerMock = new Mock<ILogger<RecordingService>>();
            _mockServiceMock = new Mock<IMockService>();
            _recordingService = new RecordingService(_loggerMock.Object, _mockServiceMock.Object);
        }

        [Fact]
        public async Task StartRecordingAsync_WithValidOptions_ShouldStartSuccessfully()
        {
            // Arrange
            var options = new RecordingOptions
            {
                TargetUrl = "https://api.example.com",
                SaveMapping = true,
                SaveMappingToFile = "test-mapping.json"
            };

            _mockServiceMock.Setup(x => x.IsServerRunning()).Returns(true);

            // Act
            var result = await _recordingService.StartRecordingAsync(options);

            // Assert
            Assert.True(result);
            Assert.True(_recordingService.IsRecording());
        }

        [Fact]
        public async Task StartRecordingAsync_WhenServerNotRunning_ShouldReturnFalse()
        {
            // Arrange
            var options = new RecordingOptions
            {
                TargetUrl = "https://api.example.com"
            };

            _mockServiceMock.Setup(x => x.IsServerRunning()).Returns(false);

            // Act
            var result = await _recordingService.StartRecordingAsync(options);

            // Assert
            Assert.False(result);
            Assert.False(_recordingService.IsRecording());
        }

        [Fact]
        public async Task StopRecordingAsync_WhenRecording_ShouldStopSuccessfully()
        {
            // Arrange
            var options = new RecordingOptions
            {
                TargetUrl = "https://api.example.com"
            };

            _mockServiceMock.Setup(x => x.IsServerRunning()).Returns(true);
            await _recordingService.StartRecordingAsync(options);

            // Act
            var result = await _recordingService.StopRecordingAsync();

            // Assert
            Assert.True(result);
            Assert.False(_recordingService.IsRecording());
        }

        [Fact]
        public async Task StopRecordingAsync_WhenNotRecording_ShouldReturnTrue()
        {
            // Act
            var result = await _recordingService.StopRecordingAsync();

            // Assert
            Assert.True(result);
            Assert.False(_recordingService.IsRecording());
        }
    }
}