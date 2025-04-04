# Mock Service for PCF with WireMock.NET

A PCF-deployable service that mocks external and internal HTTP APIs using WireMock.NET. This service provides a REST API and Swagger UI to manage mocks; in full-feature mode, it also supports recording live API traffic, but a minimal build may have this disabled.

## Features

- Mock external and internal HTTP services
- *Optional:* Record and replay HTTP interactions
- DocuSign API mocking capabilities
- Swagger UI for API documentation and testing
- Environment-specific configurations (DEV, QA)
- REST API for programmatic control
- Configurable mock responses and behaviors
- Support for stateful mocking scenarios
- Request/response pattern matching
- Dynamic response templating with Handlebars.NET
- Organized folder structure for mock responses, easily maintainable

## Prerequisites

- .NET 8.0 SDK or later
- Cloud Foundry CLI
- Access to a PCF environment
- Visual Studio 2022 or VS Code (recommended)
- WireMock.NET NuGet package included automatically
- Environment variables or vars-*.yml files configured per environment (see below)

## Getting Started

### Local Development

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd mock-netpcf
   ```

2. Install dependencies:
   ```bash
   cd src/MockNetPcf.Api
   dotnet restore
   ```

3. Build the application:
   ```bash
   dotnet build
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

5. Access the application:
   - API: http://localhost:5000
   - Swagger UI: http://localhost:5000/swagger
   - WireMock Server: http://localhost:9090
   - WireMock Admin: http://localhost:9091

### PCF Deployment
1. Build and publish:
   ```bash
   dotnet publish -c Release
   ```

2. Deploy to environment:
   ```bash
   # For DEV
   cf push --vars-file vars-dev.yml
   
   # For QA
   cf push --vars-file vars-qa.yml
   ```

## API Reference
### Mock Management
```bash
# Start mock server
POST /api/mock/start
Response: 200 OK (success), 400 Bad Request (failure)

# Stop mock server
POST /api/mock/stop
Response: 200 OK (success), 400 Bad Request (failure)

# Get server status
GET /api/mock/status
Response: 200 OK with {"isRunning": true|false}

# Add new mock
POST /api/mock
Content-Type: application/json
{
    "path": "/api/resource",
    "method": "GET",
    "statusCode": 200,
    "requestHeaders": {"Content-Type": "application/json"},
    "requestBody": "{\"id\": 1}",
    "responseHeaders": {"Content-Type": "application/json"},
    "responseBody": "{\"message\": \"Success\"}"
}
Response: 200 OK (success), 400 Bad Request (failure)

# Get all mocks
GET /api/mock
Response: 200 OK with array of mock definitions

# Remove mock
DELETE /api/mock/{id}
Response: 200 OK (success), 400 Bad Request (failure)

# Reset all mocks
POST /api/mock/reset
Response: 200 OK (success), 400 Bad Request (failure)

# Create DocuSign mocks
POST /api/mock/docusign
Response: 200 OK (success), 400 Bad Request (failure)
```

### Recording Management
```bash
# Start recording
POST /api/recording/start
Content-Type: application/json
{
    "targetUrl": "https://api.example.com",
    "saveMapping": true,
    "saveMappingToFile": "example-mapping.json"
}
Response: 200 OK (success), 400 Bad Request (failure)

# Stop recording
POST /api/recording/stop
Response: 200 OK (success), 400 Bad Request (failure)

# Get recording status
GET /api/recording/status
Response: 200 OK with {"isRecording": true|false}
```

## Configuration
### Environment Variables
Variable | Description | Default
---------|-------------|--------
WIREMOCK_PORT | Mock server port | 9090
WIREMOCK_ADMIN_PORT | Admin interface port | 9091
WIREMOCK_STORAGE_PATH | Path for storing mock mappings | mocks
WIREMOCK_ALLOWED_ORIGINS | CORS allowed origins | *
MOCK_EXTERNAL_SERVICES | External services to mock | -
MOCK_INTERNAL_SERVICES | Internal services to mock | -
ASPNETCORE_ENVIRONMENT | Runtime environment | Production

### Environment-Specific Configuration
- vars-dev.yml : Development environment settings
- vars-qa.yml : QA environment settings
- appsettings.json : Default application settings

## Troubleshooting
### Common Issues
1. Port Conflicts
   - Check port availability (5000, 9090, 9091)
   - Modify ports in configuration if needed
   - Ensure no other mock services are running

2. PCF Deployment
   - Verify CF CLI login status
   - Check resource quotas
   - Review deployment logs: cf logs mock-netpcf --recent

3. WireMock Server
   - Verify .NET runtime compatibility
   - Check network/firewall settings
   - Review application logs

4. HTTP Status Codes
   - 200 OK: Operation completed successfully
   - 400 Bad Request: Invalid input or operation failed
   - 500 Internal Server Error: Unexpected server error

5. Mock Mappings Storage
   - By default, mappings are stored in memory
   - When ReadStaticMappings is enabled, mappings are loaded from JSON files
   - Files are stored in the '__admin/mappings' directory relative to application path
   - WatchStaticMappings allows hot-reloading of mapping files

## Best Practices
1. Mock Management
   - Use descriptive names for mock definitions
   - Implement proper request matching
   - Keep mock responses minimal
   - Regular cleanup of unused mocks

2. Recording
   - Plan recording sessions
   - Verify target service availability
   - Review and clean recorded mappings
   - Test recorded scenarios

3. Security
   - Secure admin endpoints
   - Configure CORS appropriately
   - Use HTTPS in production
   - Regular credential rotation

## Project Structure
```plaintext
mock-netpcf/
├── src/
│   ├── MockNetPcf.Api/
│   │   ├── Controllers/
│   │   │   ├── MockController.cs       # Endpoints for mock management
│   │   │   └── RecordingController.cs  # Endpoints for recording management
│   │   ├── DocuSignMocks/              # DocuSign mock response templates
│   │   │   ├── DocumentsResponse.json  # Mock for documents endpoint
│   │   │   ├── EnvelopeResponse.json   # Mock for envelope endpoint
│   │   │   ├── RecipientsResponse.json # Mock for recipients endpoint
│   │   │   └── TemplatesResponse.json  # Mock for templates endpoint
│   │   ├── Services/
│   │   │   ├── MockService.cs          # WireMock server management
│   │   │   ├── RecordingService.cs     # Recording functionality
│   │   │   └── DocuSignRecordingHelper.cs # DocuSign recording helper
│   │   ├── Models/
│   │   │   ├── MockDefinition.cs       # Mock request/response definition
│   │   │   ├── RecordingOptions.cs     # Recording configuration options
│   │   │   ├── DocuSignMockManager.cs  # DocuSign mock management
│   │   │   └── DocuSignMockExample.cs  # DocuSign mock examples
│   │   ├── Configuration/
│   │   │   └── WireMockConfig.cs       # WireMock server configuration
│   │   └── Program.cs                  # Application bootstrap
│   └── MockNetPcf.Tests/
│       ├── Services/
│       │   ├── MockServiceTests.cs     # Unit tests for mock service
│       │   └── RecordingServiceTests.cs # Unit tests for recording service
│       └── Controllers/
│           ├── MockControllerTests.cs   # Unit tests for mock endpoints
│           └── RecordingControllerTests.cs # Unit tests for recording endpoints
├── manifest.yml                         # PCF deployment manifest
├── vars-dev.yml                         # DEV environment variables
├── vars-qa.yml                          # QA environment variables
└── README.md                            # Project documentation
```

## Contributing
1. Fork the repository
2. Create feature branch
3. Commit changes
4. Push to branch
5. Create Pull Request

## Support
- Review documentation
- Check troubleshooting guide
- Submit issues via repository
- Contact team for critical issues

## License
This project is licensed under the MIT License.