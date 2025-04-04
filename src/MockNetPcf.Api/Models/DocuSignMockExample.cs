using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MockNetPcf.Api.Models;
using MockNetPcf.Api.Services;
using Newtonsoft.Json;

namespace MockNetPcf.Api.Models
{
    /// <summary>
    /// Example class demonstrating how to create DocuSign mock endpoints
    /// </summary>
    public static class DocuSignMockExample
    {
        /// <summary>
        /// Creates a sample DocuSign envelope mock endpoint
        /// </summary>
        /// <param name="mockService">The mock service instance</param>
        /// <returns>True if mock was created successfully</returns>
        public static async Task<bool> CreateDocuSignEnvelopeMock(IMockService mockService)
        {
            // Load envelope response from JSON file
            return await DocuSignMockManager.CreateMockFromFile(
                mockService,
                "EnvelopeResponse",
                "/restapi/v2.1/accounts/{accountId}/envelopes/{envelopeId}",
                "GET",
                "DocuSign Envelope Mock");
        }
        
        /// <summary>
        /// Creates a sample DocuSign recipients mock endpoint
        /// </summary>
        /// <param name="mockService">The mock service instance</param>
        /// <returns>True if mock was created successfully</returns>
        public static async Task<bool> CreateDocuSignRecipientsMock(IMockService mockService)
        {
            // Load recipients response from JSON file
            return await DocuSignMockManager.CreateMockFromFile(
                mockService,
                "RecipientsResponse",
                "/restapi/v2.1/accounts/{accountId}/envelopes/{envelopeId}/recipients",
                "GET",
                "DocuSign Recipients Mock");
        }
        
        /// <summary>
        /// Creates a sample DocuSign documents mock endpoint
        /// </summary>
        /// <param name="mockService">The mock service instance</param>
        /// <returns>True if mock was created successfully</returns>
        public static async Task<bool> CreateDocuSignDocumentsMock(IMockService mockService)
        {
            // Load documents response from JSON file
            return await DocuSignMockManager.CreateMockFromFile(
                mockService,
                "DocumentsResponse",
                "/restapi/v2.1/accounts/{accountId}/envelopes/{envelopeId}/documents",
                "GET",
                "DocuSign Documents Mock");
        }
        
        /// <summary>
        /// Creates a sample DocuSign templates mock endpoint
        /// </summary>
        /// <param name="mockService">The mock service instance</param>
        /// <returns>True if mock was created successfully</returns>
        public static async Task<bool> CreateDocuSignTemplatesMock(IMockService mockService)
        {
            // Load templates response from JSON file
            return await DocuSignMockManager.CreateMockFromFile(
                mockService,
                "TemplatesResponse",
                "/restapi/v2.1/accounts/{accountId}/templates",
                "GET",
                "DocuSign Templates Mock");
        }
        
        /// <summary>
        /// Creates all DocuSign mock endpoints
        /// </summary>
        /// <param name="mockService">The mock service instance</param>
        /// <returns>True if all mocks were created successfully</returns>
        public static async Task<bool> CreateAllDocuSignMocks(IMockService mockService)
        {
            bool envelopeResult = await CreateDocuSignEnvelopeMock(mockService);
            bool recipientsResult = await CreateDocuSignRecipientsMock(mockService);
            bool documentsResult = await CreateDocuSignDocumentsMock(mockService);
            bool templatesResult = await CreateDocuSignTemplatesMock(mockService);
            
            return envelopeResult && recipientsResult && documentsResult && templatesResult;
        }
    }
}