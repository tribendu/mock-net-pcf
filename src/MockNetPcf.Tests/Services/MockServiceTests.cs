using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MockNetPcf.Api.Configuration;
using MockNetPcf.Api.Services;
using Moq;
using Xunit;

namespace MockNetPcf.Tests.Services
{
    public class MockServiceTests
    {
        private readonly Mock<ILogger<MockService>> _loggerMock;
        private readonly Mock<IOptions<WireMockConfig>> _configMock;
        private readonly WireMockConfig _config;
        private readonly MockService _mockService;

        public MockServiceTests()
        {
            _loggerMock = new Mock<ILogger<MockService>>();
            _config = new WireMockConfig { Port = 9090, StartAdminInterface = true, AdminPort = 9091 };
            _configMock = new Mock<IOptions<WireMockConfig>>();
            _configMock.Setup(x => x.Value).Returns(_config);
            _mockService = new MockService(_loggerMock.Object, _configMock.Object);
        }

        [Fact]
        public async Task StartServerAsync_WhenServerNotRunning_ShouldStartSuccessfully()
        {
            // Act
            var result = await _mockService.StartServerAsync();

            // Assert
            Assert.True(result);
            Assert.True(_mockService.IsServerRunning());
        }

        [Fact]
        public async Task StartServerAsync_WhenServerAlreadyRunning_ShouldReturnTrue()
        {
            // Arrange
            await _mockService.StartServerAsync();

            // Act
            var result = await _mockService.StartServerAsync();

            // Assert
            Assert.True(result);
            Assert.True(_mockService.IsServerRunning());
        }

        [Fact]
        public async Task StopServerAsync_WhenServerRunning_ShouldStopSuccessfully()
        {
            // Arrange
            await _mockService.StartServerAsync();

            // Act
            var result = await _mockService.StopServerAsync();

            // Assert
            Assert.True(result);
            Assert.False(_mockService.IsServerRunning());
        }

        [Fact]
        public async Task StopServerAsync_WhenServerNotRunning_ShouldReturnTrue()
        {
            // Act
            var result = await _mockService.StopServerAsync();

            // Assert
            Assert.True(result);
            Assert.False(_mockService.IsServerRunning());
        }
    }
}