using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MockNetPcf.Api.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MockNetPcf.Api.Models
{
    /// <summary>
    /// Manager class for DocuSign mock responses
    /// Handles loading and saving DocuSign mock responses from/to JSON files
    /// </summary>
    public static class DocuSignMockManager
    {
        private const string DocuSignMocksFolder = "DocuSignMocks";
        
        /// <summary>
        /// Gets the full path to the DocuSign mocks folder
        /// </summary>
        /// <returns>The full path to the DocuSign mocks folder</returns>
        public static string GetDocuSignMocksFolder()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string mocksFolder = Path.Combine(baseDirectory, DocuSignMocksFolder);
            
            // Create the directory if it doesn't exist
            if (!Directory.Exists(mocksFolder))
            {
                Directory.CreateDirectory(mocksFolder);
            }
            
            return mocksFolder;
        }
        
        /// <summary>
        /// Loads a DocuSign mock response from a JSON file
        /// </summary>
        /// <param name="fileName">The name of the JSON file (without extension)</param>
        /// <returns>The deserialized object or null if the file doesn't exist</returns>
        public static object LoadMockResponse(string fileName)
        {
            string filePath = Path.Combine(GetDocuSignMocksFolder(), $"{fileName}.json");
            
            if (!File.Exists(filePath))
            {
                return null;
            }
            
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject(json);
        }
        
        /// <summary>
        /// Saves a DocuSign mock response to a JSON file
        /// </summary>
        /// <param name="fileName">The name of the JSON file (without extension)</param>
        /// <param name="response">The response object to save</param>
        /// <returns>True if the response was saved successfully, false otherwise</returns>
        public static bool SaveMockResponse(string fileName, object response)
        {
            try
            {
                string filePath = Path.Combine(GetDocuSignMocksFolder(), $"{fileName}.json");
                string json = JsonConvert.SerializeObject(response, Formatting.Indented);
                File.WriteAllText(filePath, json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        /// <summary>
        /// Creates a DocuSign mock endpoint from a JSON file
        /// </summary>
        /// <param name="mockService">The mock service instance</param>
        /// <param name="fileName">The name of the JSON file (without extension)</param>
        /// <param name="path">The API path for the mock endpoint</param>
        /// <param name="method">The HTTP method for the mock endpoint</param>
        /// <param name="name">The name of the mock endpoint</param>
        /// <returns>True if the mock was created successfully, false otherwise</returns>
        public static async Task<bool> CreateMockFromFile(IMockService mockService, string fileName, string path, string method = "GET", string name = null)
        {
            object response = LoadMockResponse(fileName);
            
            if (response == null)
            {
                return false;
            }
            
            var mockDefinition = new MockDefinition
            {
                Id = Guid.NewGuid().ToString(),
                Name = name ?? $"DocuSign {fileName} Mock",
                Path = path,
                Method = method,
                RequestHeaders = new Dictionary<string, string>
                {
                    { "Authorization", "Bearer *" }
                },
                RequestBody = "",
                StatusCode = 200,
                ResponseHeaders = new Dictionary<string, string>
                {
                    { "Content-Type", "application/json" },
                    { "X-DocuSign-TraceToken", Guid.NewGuid().ToString() }
                },
                ResponseBody = JsonConvert.SerializeObject(response, Formatting.Indented),
                ErrorMessage = null
            };
            
            return await mockService.AddMockAsync(mockDefinition);
        }
        
        /// <summary>
        /// Saves a mock response from a MockDefinition
        /// </summary>
        /// <param name="mockDefinition">The mock definition containing the response</param>
        /// <param name="fileName">The name of the JSON file (without extension)</param>
        /// <returns>True if the response was saved successfully, false otherwise</returns>
        public static bool SaveMockFromDefinition(MockDefinition mockDefinition, string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(mockDefinition.ResponseBody))
                {
                    return false;
                }
                
                object responseObj = JsonConvert.DeserializeObject(mockDefinition.ResponseBody);
                return SaveMockResponse(fileName, responseObj);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}