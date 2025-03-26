# Mock Service for PCF with WireMock.NET

A PCF-deployable service that mocks external and internal services using WireMock.NET. This service provides a REST API and Swagger UI to manage mocks and control recording functionality.

## Features

- Mock external and internal HTTP services
- Record and replay HTTP interactions
- Swagger UI for API documentation and testing
- Environment-specific configurations (DEV, QA)
- REST API for programmatic control
- Configurable mock responses and behaviors
- Support for stateful mocking scenarios
- Request/response pattern matching
- Dynamic response templating

## Prerequisites

- .NET 6.0 SDK or later
- Cloud Foundry CLI
- Access to a PCF environment
- Visual Studio 2022 or VS Code (recommended)

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

# Stop mock server
POST /api/mock/stop

# Get server status
GET /api/mock/status

# Add new mock
POST /api/mock
Content-Type: application/json
{
    "path": "/api/resource",
    "method": "GET",
    "response": {
        "status": 200,
        "body": {"message": "Success"}
    }
}
```

### Recording Management
```bash
# Start recording
POST /api/recording/start
Content-Type: application/json
{
    "targetUrl": "https://api.example.com",
    "saveMappingToFile": "example-mapping.json"
}

# Stop recording
POST /api/recording/stop

# Get recording status
GET /api/recording/status
```

## Configuration
### Environment Variables
Variable | Description | Default
---------|-------------|--------
WIREMOCK_PORT | Mock server port | 9090
WIREMOCK_ADMIN_PORT | Admin interface port | 9091
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
│   │   ├── Services/
│   │   ├── Models/
│   │   └── Configuration/
│   └── MockNetPcf.Tests/
├── manifest.yml
├── vars-dev.yml
├── vars-qa.yml
└── README.md
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