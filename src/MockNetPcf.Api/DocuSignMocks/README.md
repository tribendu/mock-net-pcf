# DocuSign Mock Responses

This folder contains JSON response templates for various DocuSign API endpoints. These templates are used by the `DocuSignMockManager` to create mock endpoints that simulate DocuSign API responses.

## Available Mock Responses

- **EnvelopeResponse.json**: Mock response for the envelope endpoint (`/restapi/v2.1/accounts/{accountId}/envelopes/{envelopeId}`)
- **RecipientsResponse.json**: Mock response for the recipients endpoint (`/restapi/v2.1/accounts/{accountId}/envelopes/{envelopeId}/recipients`)
- **DocumentsResponse.json**: Mock response for the documents endpoint (`/restapi/v2.1/accounts/{accountId}/envelopes/{envelopeId}/documents`)

## Adding New Mock Responses

To add a new mock response:

1. Create a new JSON file in this folder with an appropriate name (e.g., `TemplatesResponse.json`)
2. Add the JSON response content to the file
3. Update the `DocuSignMockExample` class to include a method that creates a mock endpoint using the new response file

## Using the DocuSignMockManager

The `DocuSignMockManager` class provides methods for loading and saving DocuSign mock responses:

```csharp
// Load a mock response from a file
object response = DocuSignMockManager.LoadMockResponse("EnvelopeResponse");

// Save a mock response to a file
DocuSignMockManager.SaveMockResponse("NewResponse", responseObject);

// Create a mock endpoint from a file
await DocuSignMockManager.CreateMockFromFile(
    mockService,
    "EnvelopeResponse",
    "/restapi/v2.1/accounts/{accountId}/envelopes/{envelopeId}",
    "GET",
    "DocuSign Envelope Mock");

// Save a mock definition to a file
DocuSignMockManager.SaveMockFromDefinition(mockDefinition, "NewResponse");
```