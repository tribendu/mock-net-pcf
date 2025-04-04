# Repository Metadata

This document provides an overview of the repository structure, file contents, and relationships between components in the mock-netpcf project.

## Project Overview

The mock-netpcf project is a PCF-deployable service that uses WireMock.NET to mock external and internal services. It provides a REST API and Swagger UI for managing mocks and controlling recording functionality.

## Folder Structure

mock-netpcf/
├── src/                                # Source code directory
│   ├── MockNetPcf.Api/                 # Main API project
│   │   ├── Controllers/                # API controllers
│   │   │   ├── MockController.cs       # Endpoints for mock management
│   │   │   └── RecordingController.cs  # Endpoints for recording management
│   │   ├── Services/                   # Business logic services
│   │   │   ├── MockService.cs          # WireMock server management
│   │   │   └── RecordingService.cs     # Recording functionality
│   │   ├── Models/                     # Data models
│   │   │   ├── MockDefinition.cs       # Mock request/response definition
│   │   │   └── RecordingOptions.cs     # Recording configuration options
│   │   ├── Configuration/              # Configuration classes
│   │   │   └── WireMockConfig.cs       # WireMock server configuration
│   │   ├── Program.cs                  # Application entry point
│   │   └── appsettings.json            # Application settings
│   └── MockNetPcf.Tests/               # Test project
│       ├── Controllers/                # Controller tests
│       ├── Services/                   # Service tests
│       └── MockNetPcf.Tests.csproj     # Test project file
├── manifest.yml                        # PCF deployment manifest
├── vars-dev.yml                        # DEV environment variables
├── vars-qa.yml                         # QA environment variables
├── Example.md                          # DocuSign API mock examples
├── README.md                           # Project documentation
├── repoMetaData.md                     # This file
└── .gitignore                          # Git ignore configuration

## Key Files and Their Contents

### Configuration Files

- **manifest.yml**: PCF deployment manifest that defines the application name, memory allocation, instances, and environment variables.
- **vars-dev.yml**: Environment-specific variables for the DEV environment, including service URLs and port configurations.
- **vars-qa.yml**: Environment-specific variables for the QA environment, similar to vars-dev.yml but with QA-specific values.
- **appsettings.json**: Application configuration including logging settings and WireMock configuration.

### Source Code

#### Controllers

- **MockController.cs**: REST API endpoints for managing the mock server, including:
  - Starting/stopping the server
  - Adding/removing mock definitions
  - Getting server status
  - Resetting mocks

- **RecordingController.cs**: REST API endpoints for controlling recording functionality, including:
  - Starting/stopping recording
  - Getting recording status

#### Services

- **MockService.cs**: Core service that manages the WireMock server, implementing:
  - Server lifecycle management
  - Mock definition management
  - Server status reporting

- **RecordingService.cs**: Service that handles recording functionality, implementing:
  - Recording session management
  - Proxy configuration
  - Recording status reporting

#### Models

- **MockDefinition.cs**: Data model for mock definitions, including:
  - Request matching criteria (path, method, headers, body)
  - Response configuration (status code, headers, body)

- **RecordingOptions.cs**: Data model for recording options, including:
  - Target URL to record from
  - Mapping save options

#### Configuration

- **WireMockConfig.cs**: Configuration class for WireMock server settings, including:
  - Port configuration
  - Admin interface settings
  - CORS configuration
  - Storage path

### Documentation

- **README.md**: Main project documentation with overview, setup instructions, and usage examples.
- **Example.md**: Detailed examples of mocking DocuSign API endpoints.
- **repoMetaData.md**: This file, documenting the repository structure and relationships.

## Component Relationships

### Dependency Flow

1. **Program.cs** bootstraps the application and configures dependency injection
2. **Controllers** depend on **Services** for business logic
3. **Services** use **Models** for data structures
4. **Services** are configured using **WireMockConfig**
5. **WireMockConfig** is populated from **appsettings.json** and environment variables

### Request Flow

1. Client sends request to API endpoints in **Controllers**
2. **Controllers** delegate to appropriate **Services**
3. **Services** interact with WireMock.NET library
4. WireMock.NET handles mock responses or recording

### Deployment Flow

1. Application is built and published
2. **manifest.yml** defines the application for PCF
3. Environment-specific variables from **vars-dev.yml** or **vars-qa.yml** are applied
4. PCF deploys the application with the specified configuration

## Environment Configuration

The application supports multiple environments through PCF's variable substitution:

1. **manifest.yml** contains placeholders like `((environment))` and `((wiremock_port))`
2. **vars-dev.yml** and **vars-qa.yml** provide values for these placeholders
3. When deploying with `cf push --vars-file vars-dev.yml`, the DEV values are substituted
4. When deploying with `cf push --vars-file vars-qa.yml`, the QA values are substituted