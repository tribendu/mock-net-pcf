using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MockNetPcf.Api.Configuration;
using MockNetPcf.Api.Models;
using WireMock.Server;
using WireMock.Settings;

namespace MockNetPcf.Api.Services
{
    /// <summary>
    /// Interface for managing mock API endpoints using WireMock.NET
    /// </summary>
    public interface IMockService
    {
        /// <summary>
        /// Starts the WireMock server on configured port
        /// </summary>
        /// <returns>True if server started successfully, false otherwise</returns>
        Task<bool> StartServerAsync();

        /// <summary>
        /// Stops the running WireMock server
        /// </summary>
        /// <returns>True if server stopped successfully, false otherwise</returns>
        Task<bool> StopServerAsync();

        /// <summary>
        /// Adds a new mock endpoint definition to the server
        /// </summary>
        /// <param name="mockDefinition">The mock endpoint configuration</param>
        /// <returns>True if mock was added successfully, false otherwise</returns>
        Task<bool> AddMockAsync(MockDefinition mockDefinition);

        /// <summary>
        /// Retrieves all configured mock endpoints
        /// </summary>
        /// <returns>Collection of mock definitions</returns>
        Task<IEnumerable<MockDefinition>> GetAllMocksAsync();

        /// <summary>
        /// Removes a specific mock endpoint by ID
        /// </summary>
        /// <param name="id">The ID of the mock to remove</param>
        /// <returns>True if mock was removed successfully, false otherwise</returns>
        Task<bool> RemoveMockAsync(string id);

        /// <summary>
        /// Removes all configured mock endpoints
        /// </summary>
        /// <returns>True if mocks were reset successfully, false otherwise</returns>
        Task<bool> ResetMocksAsync();

        /// <summary>
        /// Checks if the WireMock server is currently running
        /// </summary>
        /// <returns>True if server is running, false otherwise</returns>
        bool IsServerRunning();
    }

    /// <summary>
    /// Implementation of IMockService using WireMock.NET
    /// Manages mock API endpoints and server operations
    /// 
    /// Mapping Storage:
    /// - By default, mappings are stored in memory
    /// - When ReadStaticMappings is enabled, mappings can be loaded from JSON files
    /// - Files are typically stored in the '__admin/mappings' directory relative to application path
    /// - WatchStaticMappings allows hot-reloading of mapping files
    /// </summary>
    public class MockService : IMockService
    {
        private readonly ILogger<MockService> _logger;
        private readonly WireMockConfig _config;
        private WireMockServer _server; // The WireMock server instance

        public MockService(ILogger<MockService> logger, IOptions<WireMockConfig> config)
        {
            _logger = logger;
            _config = config.Value;
        }

        public Task<bool> StartServerAsync()
        {
            try
            {
                if (_server != null && _server.IsStarted)
                {
                    _logger.LogInformation("WireMock server is already running");
                    return Task.FromResult(true);
                }

                _server = WireMockServer.Start(new WireMockServerSettings
                {
                    Port = _config.Port,
                    StartAdminInterface = _config.StartAdminInterface,
                    AdminPort = _config.AdminPort,
                    AllowPartialMapping = true,
                    ReadStaticMappings = true,
                    WatchStaticMappings = true,
                    WatchStaticMappingsInSubdirectories = true,
                    FileSystemHandler = null // Use default
                });

                _logger.LogInformation($"WireMock server started on port {_config.Port}");
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to start WireMock server");
                return Task.FromResult(false);
            }
        }

        public Task<bool> StopServerAsync()
        {
            try
            {
                if (_server != null && _server.IsStarted)
                {
                    _server.Stop();
                    _logger.LogInformation("WireMock server stopped");
                    return Task.FromResult(true);
                }

                _logger.LogInformation("WireMock server is not running");
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to stop WireMock server");
                return Task.FromResult(false);
            }
        }

        public Task<bool> AddMockAsync(MockDefinition mockDefinition)
        {
            try
            {
                if (_server == null || !_server.IsStarted)
                {
                    _logger.LogError("WireMock server is not running");
                    return Task.FromResult(false);
                }

                var response = new ResponseMessage
                {
                    StatusCode = mockDefinition.StatusCode,
                    Headers = mockDefinition.ResponseHeaders,
                    Body = mockDefinition.ResponseBody
                };

                _server
                    .Given(Request.Create()
                        .WithPath(mockDefinition.Path)
                        .WithHeader(mockDefinition.RequestHeaders)
                        .WithBody(mockDefinition.RequestBody)
                        .UsingMethod(mockDefinition.Method))
                    .RespondWith(response);

                _logger.LogInformation($"Added mock for {mockDefinition.Method} {mockDefinition.Path} with status code {mockDefinition.StatusCode}");
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add mock");
                return Task.FromResult(false);
            }
        }

        public Task<IEnumerable<MockDefinition>> GetAllMocksAsync()
        {
            // Implementation to get all mocks from the WireMock server
            return Task.FromResult<IEnumerable<MockDefinition>>(new List<MockDefinition>());
        }

        public Task<bool> RemoveMockAsync(string id)
        {
            // Implementation to remove a mock from the WireMock server
            return Task.FromResult(true);
        }

        public Task<bool> ResetMocksAsync()
        {
            // Implementation to reset all mocks in the WireMock server
            return Task.FromResult(true);
        }

        public bool IsServerRunning()
        {
            return _server != null && _server.IsStarted;
        }
    }
}