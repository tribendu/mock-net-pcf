# DocuSign API Mock Examples

This document provides examples for mocking DocuSign API endpoints using the Mock Service for PCF with WireMock.NET. These examples cover common DocuSign operations and can be used as templates for creating your own mock definitions.

## Table of Contents

1. [Authentication](#authentication)
2. [Envelope Operations](#envelope-operations)
3. [Template Operations](#template-operations)
4. [Document Operations](#document-operations)
5. [User Operations](#user-operations)

## Authentication

### OAuth2 Token Request

```bash
# Add mock for OAuth2 token endpoint
POST /api/mock
Content-Type: application/json
{
    "name": "DocuSign OAuth2 Token",
    "path": "/oauth/token",
    "method": "POST",
    "requestHeaders": {
        "Content-Type": "application/x-www-form-urlencoded"
    },
    "requestBody": "grant_type=authorization_code&code=*",
    "statusCode": 200,
    "responseHeaders": {
        "Content-Type": "application/json"
    },
    "responseBody": "{
        \"access_token\": \"eyJ0eXAiOiJNVCIsImFsZyI6IlJTMjU2Iiwia2lkIjoiNjgxODVmZjEtNGU1MS00Y2U5LWFmMWMtNjg5ODEyMjAzMzE3In0.mocktoken\",
        \"token_type\": \"Bearer\",
        \"refresh_token\": \"eyJ0eXAiOiJNVCIsImFsZyI6IlJTMjU2Iiwia2lkIjoiNjgxODVmZjEtNGU1MS00Y2U5LWFmMWMtNjg5ODEyMjAzMzE3In0.mockrefresh\",
        \"expires_in\": 28800
    }"
}
```

### JWT Authentication

```bash
# Add mock for JWT authentication
POST /api/mock
Content-Type: application/json
{
    "name": "DocuSign JWT Auth",
    "path": "/oauth/token",
    "method": "POST",
    "requestHeaders": {
        "Content-Type": "application/x-www-form-urlencoded"
    },
    "requestBody": "grant_type=urn:ietf:params:oauth:grant-type:jwt-bearer&assertion=*",
    "statusCode": 200,
    "responseHeaders": {
        "Content-Type": "application/json"
    },
    "responseBody": "{
        \"access_token\": \"eyJ0eXAiOiJNVCIsImFsZyI6IlJTMjU2Iiwia2lkIjoiNjgxODVmZjEtNGU1MS00Y2U5LWFmMWMtNjg5ODEyMjAzMzE3In0.mockjwttoken\",
        \"token_type\": \"Bearer\",
        \"expires_in\": 3600
    }"
}
```

## Envelope Operations

### Create Envelope

```bash
# Add mock for creating an envelope
POST /api/mock
Content-Type: application/json
{
    "name": "DocuSign Create Envelope",
    "path": "/v2.1/accounts/*/envelopes",
    "method": "POST",
    "requestHeaders": {
        "Content-Type": "application/json",
        "Authorization": "Bearer *"
    },
    "statusCode": 201,
    "responseHeaders": {
        "Content-Type": "application/json"
    },
    "responseBody": "{
        \"envelopeId\": \"aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee\",
        \"uri\": \"/envelopes/aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee\",
        \"statusDateTime\": \"2023-01-01T12:00:00.0000000Z\",
        \"status\": \"created\"
    }"
}
```

### Get Envelope Status

```bash
# Add mock for getting envelope status
POST /api/mock
Content-Type: application/json
{
    "name": "DocuSign Get Envelope Status",
    "path": "/v2.1/accounts/*/envelopes/aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
    "method": "GET",
    "requestHeaders": {
        "Authorization": "Bearer *"
    },
    "statusCode": 200,
    "responseHeaders": {
        "Content-Type": "application/json"
    },
    "responseBody": "{
        \"status\": \"sent\",
        \"documentsUri\": \"/envelopes/aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee/documents\",
        \"recipientsUri\": \"/envelopes/aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee/recipients\",
        \"envelopeUri\": \"/envelopes/aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee\",
        \"envelopeId\": \"aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee\",
        \"customFieldsUri\": \"/envelopes/aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee/custom_fields\",
        \"emailSubject\": \"Please sign this document\",
        \"createdDateTime\": \"2023-01-01T12:00:00.0000000Z\",
        \"sentDateTime\": \"2023-01-01T12:05:00.0000000Z\",
        \"statusChangedDateTime\": \"2023-01-01T12:05:00.0000000Z\"
    }"
}
```

### List Envelope Recipients

```bash
# Add mock for listing envelope recipients
POST /api/mock
Content-Type: application/json
{
    "name": "DocuSign List Envelope Recipients",
    "path": "/v2.1/accounts/*/envelopes/aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee/recipients",
    "method": "GET",
    "requestHeaders": {
        "Authorization": "Bearer *"
    },
    "statusCode": 200,
    "responseHeaders": {
        "Content-Type": "application/json"
    },
    "responseBody": "{
        \"signers\": [
            {
                \"name\": \"John Doe\",
                \"email\": \"john.doe@example.com\",
                \"recipientId\": \"1\",
                \"recipientIdGuid\": \"11111111-2222-3333-4444-555555555555\",
                \"status\": \"sent\"
            },
            {
                \"name\": \"Jane Smith\",
                \"email\": \"jane.smith@example.com\",
                \"recipientId\": \"2\",
                \"recipientIdGuid\": \"66666666-7777-8888-9999-000000000000\",
                \"status\": \"pending\"
            }
        ],
        \"agents\": [],
        \"editors\": [],
        \"intermediaries\": [],
        \"carbonCopies\": [],
        \"certifiedDeliveries\": [],
        \"inPersonSigners\": [],
        \"recipientCount\": 2
    }"
}
```

## Template Operations

### List Templates

```bash
# Add mock for listing templates
POST /api/mock
Content-Type: application/json
{
    "name": "DocuSign List Templates",
    "path": "/v2.1/accounts/*/templates",
    "method": "GET",
    "requestHeaders": {
        "Authorization": "Bearer *"
    },
    "statusCode": 200,
    "responseHeaders": {
        "Content-Type": "application/json"
    },
    "responseBody": "{
        \"resultSetSize\": 2,
        \"totalSetSize\": 2,
        \"startPosition\": 0,
        \"endPosition\": 1,
        \"templates\": [
            {
                \"templateId\": \"11111111-2222-3333-4444-555555555555\",
                \"name\": \"Contract Template\",
                \"shared\": false,
                \"createdDateTime\": \"2022-01-01T10:00:00.0000000Z\"
            },
            {
                \"templateId\": \"66666666-7777-8888-9999-000000000000\",
                \"name\": \"NDA Template\",
                \"shared\": true,
                \"createdDateTime\": \"2022-02-01T11:00:00.0000000Z\"
            }
        ]
    }"
}
```

### Get Template Details

```bash
# Add mock for getting template details
POST /api/mock
Content-Type: application/json
{
    "name": "DocuSign Get Template",
    "path": "/v2.1/accounts/*/templates/11111111-2222-3333-4444-555555555555",
    "method": "GET",
    "requestHeaders": {
        "Authorization": "Bearer *"
    },
    "statusCode": 200,
    "responseHeaders": {
        "Content-Type": "application/json"
    },
    "responseBody": "{
        \"templateId\": \"11111111-2222-3333-4444-555555555555\",
        \"name\": \"Contract Template\",
        \"shared\": false,
        \"emailSubject\": \"Please sign this contract\",
        \"status\": \"active\",
        \"createdDateTime\": \"2022-01-01T10:00:00.0000000Z\",
        \"lastModifiedDateTime\": \"2022-01-10T14:30:00.0000000Z\",
        \"documents\": [
            {
                \"documentId\": \"1\",
                \"name\": \"Contract.pdf\",
                \"order\": 1,
                \"pages\": 5
            }
        ],
        \"recipients\": {
            \"signers\": [
                {
                    \"recipientId\": \"1\",
                    \"roleName\": \"Signer 1\",
                    \"routingOrder\": 1
                },
                {
                    \"recipientId\": \"2\",
                    \"roleName\": \"Signer 2\",
                    \"routingOrder\": 2
                }
            ],
            \"carbonCopies\": [
                {
                    \"recipientId\": \"3\",
                    \"roleName\": \"CC\",
                    \"routingOrder\": 3
                }
            ]
        }
    }"
}
```

## Document Operations

### Get Document from Envelope

```bash
# Add mock for getting a document from an envelope
POST /api/mock
Content-Type: application/json
{
    "name": "DocuSign Get Document",
    "path": "/v2.1/accounts/*/envelopes/aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee/documents/1",
    "method": "GET",
    "requestHeaders": {
        "Authorization": "Bearer *"
    },
    "statusCode": 200,
    "responseHeaders": {
        "Content-Type": "application/pdf",
        "Content-Disposition": "attachment; filename=\"document.pdf\""
    },
    "responseBody": "JVBERi0xLjcKJeLjz9MKNSAwIG9iago8PC9GaWx0ZXIvRmxhdGVEZWNvZGUvTGVuZ3RoIDM4Pj5zdHJlYW0KeJwr5HIK4TI2UwhWMFAwMDJUCEnhcg3lCuQqVshLzE01NFEwAEIjQyMFrkgFIwMuhxCuQADerQoJCmVuZHN0cmVhbQplbmRvYmoKNiAwIG9iago8PC9GaWx0ZXIvRmxhdGVEZWNvZGUvTGVuZ3RoIDIzOT4+c3RyZWFtCnicK+RyCuEyNlMIUTBQMDJSCErhcg3lCuQqVkhJLEnVK0ktLlEoLs3NTSxKzkgtSgWKGBgqhCTmFOcXpQM5iUXJ+UW6ZYk5palAeQOFpMScnEqF4pLEolSF4NLiktKinMw8hZLUvBKgZKRCUX5xZnJJZn4eUE9iSSZQZ3JpTk5+eWlOCVBPBdAEoB6gkaUKJflFqakpQFsLgOYCxQz0jI0UQhLzgGYkF5XmJQPFgOYUlySWpILMTC0uAZqfWpQOlAU5BuQkoLNA3gD5QM9Yz9hAz8RMz9BEz9DEQs/IVI8rEuRsA6BbDQyBjgS6F+JmI4VQBSMFM6DAQAUAy1JoMQplbmRzdHJlYW0KZW5kb2JqCjggMCBvYmoKPDwvRmlsdGVyL0ZsYXRlRGVjb2RlL0xlbmd0aCAzMzY+PnN0cmVhbQp4nCvkcgrhMjZTCFEwUDAyUghK4XIN5QrkKlbISSxJVcjMK0lNTy1SKC7NzU0sSs5ILUoFihgYKoQk5hTnF6UDOYlFyflFumWJOaWpQHkDhaTE1OISheKSxKJUhZDU4hKF4NLiktKinMw8hZLUvBKgZKRCUX5xZnJJZn4eUE9iSSZQZ3JpTk5+eWlOCVBPBdAEoB6gkaUKJflFqakpQFsLgOYCxQz0jI0UQhLzgGYkF5XmJQPFgOYUlySWpILMTC0uAZqfWpQOlAU5BuQkoLNA3gD5QM9Yz9hAz8RMz9BEz9DEQs/IVI8rEuRsA6BbDQyBjgS6F+JmI4VQBSMFM6DAQAUjBQtTBSMLUwUjSzOgmJGRgZEFkG9qYGRpYWpgaWZkYGRuYGRhYWpqaWFmZmZkbGZpZmJkamJqYWRqZGRkYWxpbmZkZGxpYWJsaWFqaWZpYQYAVQJnvwplbmRzdHJlYW0KZW5kb2JqCjkgMCBvYmoKPDwvRmlsdGVyL0ZsYXRlRGVjb2RlL0xlbmd0aCAxOTk+PnN0cmVhbQp4nCvkcgrhMjZTCFEwUDAyUghK4XIN5QrkKlYoSC1KLo9PzwaKGBgqhCTmFOcXpQM5iUXJ+UW6ZYk5palAeQOFpMScnEqF4pLEolSF4NLiktKinMw8hZLUvBKgZKRCUX5xZnJJZn4eUE9iSSZQZ3JpTk5+eWlOCVBPBdAEoB6gkaUKJflFqakpQFsLgOYCxQz0jI0UQhLzgGYkF5XmJQPFgOYUlySWpILMTC0uAZqfWpQOlAU5BuQkoLNA3gD5QM9Yz9hAz8RMz9BEz9DEQs/IVI8rEuRsA6BbDQyBjgS6F+JmI4VQBSMFM6DAQAUAy1JoMQplbmRzdHJlYW0KZW5kb2JqCjExIDAgb2JqCjw8L0ZpbHRlci9GbGF0ZURlY29kZS9MZW5ndGggMjE1Pj5zdHJlYW0KeJwr5HIK4TI2UwhRMFAwMlIISuFyDeUK5CpWKEotLs5MzlAoLs3NTSxKzkgtSgWKGBgqhCTmFOcXpQM5iUXJ+UW6ZYk5palAeQOFpMScnEqF4pLEolSF4NLiktKinMw8hZLUvBKgZKRCUX5xZnJJZn4eUE9iSSZQZ3JpTk5+eWlOCVBPBdAEoB6gkaUKJflFqakpQFsLgOYCxQz0jI0UQhLzgGYkF5XmJQPFgOYUlySWpILMTC0uAZqfWpQOlAU5BuQkoLNA3gD5QM9Yz9hAz8RMz9BEz9DEQs/IVI8rEuRsA6BbDQyBjgS6F+JmI4VQBSMFM6DAQAUAy1JoMQplbmRzdHJlYW0KZW5kb2JqCjEyIDAgb2JqCjw8L0ZpbHRlci9GbGF0ZURlY29kZS9MZW5ndGggMjI1Pj5zdHJlYW0KeJwr5HIK4TI2UwhRMFAwMlIISuFyDeUK5CpWKM4vKlFIzEtRKC7NzU0sSs5ILUoFihgYKoQk5hTnF6UDOYlFyflFumWJOaWpQHkDhaTE1OISheKSxKJUhZDU4hKF4NLiktKinMw8hZLUvBKgZKRCUX5xZnJJZn4eUE9iSSZQZ3JpTk5+eWlOCVBPBdAEoB6gkaUKJflFqakpQFsLgOYCxQz0jI0UQhLzgGYkF5XmJQPFgOYUlySWpILMTC0uAZqfWpQOlAU5BuQkoLNA3gD5QM9Yz9hAz8RMz9BEz9DEQs/IVI8rEuRsA6BbDQyBjgS6F+JmI4VQBSMFM6DAQAUAy1JoMQplbmRzdHJlYW0KZW5kb2JqCjE0IDAgb2JqCjw8L0ZpbHRlci9GbGF0ZURlY29kZS9MZW5ndGggMjk+PnN0cmVhbQp4nCvkcgrhMjZTCFEwAEIjQyMFrkgFIwMuhxCuQADerQoJCmVuZHN0cmVhbQplbmRvYmoKMTYgMCBvYmoKPDwvRmlsdGVyL0ZsYXRlRGVjb2RlL0xlbmd0aCAyMTU+PnN0cmVhbQp4nCvkcgrhMjZTCFEwUDAyUghK4XIN5QrkKlYoSi0uzkzOUCguzc1NLErOSC1KBYoYGCqEJOYU5xelAzmJRcn5RbpliTmlqUB5A4WkxJycSoXiksSiVIXg0uKS0qKczDyFktS8EqBkpEJRfnFmcklmfh5QT2JJJlBncmlOTn55aU4JUE8F0ASgHqCRpQol+UWpqSlAWwuA5gLFDPSMjRRCEvOAZiQXleYlA8WA5hSXJJakgsxMLS4Bmp9alA6UBTkG5CSgs0DeAPlAz1jP2EDPxEzP0ETP0MRCz8hUjysS5GwDoFsNDIGOBLoX4mYjhVAFIwUzoMBABQDLUmgxCmVuZHN0cmVhbQplbmRvYmoKMTcgMCBvYmoKPDwvRmlsdGVyL0ZsYXRlRGVjb2RlL0xlbmd0aCAyMTU+PnN0cmVhbQp4nCvkcgrhMjZTCFEwUDAyUghK4XIN5QrkKlYoSi0uzkzOUCguzc1NLErOSC1KBYoYGCqEJOYU5xelAzmJRcn5RbpliTmlqUB5A4WkxJycSoXiksSiVIXg0uKS0qKczDyFktS8EqBkpEJRfnFmcklmfh5QT2JJJlBncmlOTn55aU4JUE8F0ASgHqCRpQol+UWpqSlAWwuA5gLFDPSMjRRCEvOAZiQXleYlA8WA5hSXJJakgsxMLS4Bmp9alA6UBTkG5CSgs0DeAPlAz1jP2EDPxEzP0ETP0MRCz8hUjysS5GwDoFsNDIGOBLoX4mYjhVAFIwUzoMBABQDLUmgxCmVuZHN0cmVhbQplbmRvYmoKMTkgMCBvYmoKPDwvRmlsdGVyL0ZsYXRlRGVjb2RlL0xlbmd0aCAyMTU+PnN0cmVhbQp4nCvkcgrhMjZTCFEwUDAyUghK4XIN5QrkKlYoSi0uzkzOUCguzc1NLErOSC1KBYoYGCqEJOYU5xelAzmJRcn5RbpliTmlqUB5A4WkxJycSoXiksSiVIXg0uKS0qKczDyFktS8EqBkpEJRfnFmcklmfh5QT2JJJlBncmlOTn55aU4JUE8F0ASgHqCRpQol+UWpqSlAWwuA5gLFDPSMjRRCEvOAZiQXleYlA8WA5hSXJJakgsxMLS4Bmp9alA6UBTkG5CSgs0DeAPlAz1jP2EDPxEzP0ETP0MRCz8hUjysS5GwDoFsNDIGOBLoX4mYjhVAFIwUzoMBABQDLUmgxCmVuZHN0cmVhbQplbmRvYmoKMjEgMCBvYmoKPDwvRmlsdGVyL0ZsYXRlRGVjb2RlL0xlbmd0aCAyMTU+PnN0cmVhbQp4nCvkcgrhMjZTCFEwUDAyUghK4XIN5QrkKlYoSi0uzkzOUCguzc1NLErOSC1KBYoYGCqEJOYU5xelAzmJRcn5RbpliTmlqUB5A4WkxJycSoXiksSiVIXg0uKS0qKczDyFktS8EqBkpEJRfnFmcklmfh5QT2JJJlBncmlOTn55aU4JUE8F0ASgHqCRpQol+UWpqSlAWwuA5gLFDPSMjRRCEvOAZiQXleYlA8WA5hSXJJakgsxMLS4Bmp9alA6UBTkG5CSgs0DeAPlAz1jP2EDPxEzP0ETP0MRCz8hUjysS5GwDoFsNDIGOBLoX4mYjhVAFIwUzoMBABQDLUmgxCmVuZHN0cmVhbQplbmRvYmoKMjMgMCBvYmoKPDwvRmlsdGVyL0ZsYXRlRGVjb2RlL0xlbmd0aCAyMTU+PnN0cmVhbQp4nCvkcgrhMjZTCFEwUDAyUghK4XIN5QrkKlYoSi0uzkzOUCguzc1NLErOSC1KBYoYGCqEJOYU5xelAzmJRcn5RbpliTmlqUB5A4WkxJycSoXiksSiVIXg0uKS0qKczDyFktS8EqBkpEJRfnFmcklmfh5QT2JJJlBncmlOTn55aU4JUE8F0ASgHqCRpQol+UWpqSlAWwuA5gLFDPSMjRRCEvOAZiQXleYlA8WA5hSXJJakgsxMLS4Bmp9alA6UBTkG5CSgs0DeAPlAz1jP2EDPxEzP0ETP0MRCz8hUjysS5GwDoFsNDIGOBLoX4mYjhVAFIwUzoMBABQDLUmgxCmVuZHN0cmVhbQplbmRvYmoKMjUgMCBvYmoKPDwvRmlsdGVyL0ZsYXRlRGVjb2RlL0xlbmd0aCAyMTU+PnN0cmVhbQp4nCvkcgrhMjZTCFEwUDAyUghK4XIN5QrkKlYoSi0uzkzOUCguzc1NLEr