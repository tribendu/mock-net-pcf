using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MockNetPcf.Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MockNetPcf.Api.Services
{
    /// <summary>
    /// Helper class for saving DocuSign API responses to the DocuSignMocks folder
    /// </summary>
    public static class DocuSignRecordingHelper
    {
        private static readonly Regex DocuSignPathRegex = new Regex(@"/restapi/v\d+\.\d+/accounts/\d+/(?<endpoint>[\w/]+)", RegexOptions.Compiled);
        
        /// <summary>
        /// Processes a mock definition to save DocuSign responses to the mocks folder
        /// </summary>
        /// <param name="mockDefinition">The mock definition containing the response</param>
        /// <returns>True if the response was saved successfully, false otherwise</returns>
        public static bool ProcessDocuSignMock(MockDefinition mockDefinition)
        {
            // Check if this is a DocuSign API response
            if (mockDefinition == null || string.IsNullOrEmpty(mockDefinition.Path) || !mockDefinition.Path.Contains("/restapi/"))
            {
                return false;
            }
            
            try
            {
                // Extract the endpoint name from the path
                var match = DocuSignPathRegex.Match(mockDefinition.Path);
                if (!match.Success)
                {
                    return false;
                }
                
                string endpoint = match.Groups["endpoint"].Value;
                string fileName = GetFileNameFromEndpoint(endpoint);
                
                // Save the response to a file
                return DocuSignMockManager.SaveMockFromDefinition(mockDefinition, fileName);
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        /// <summary>
        /// Converts a DocuSign API endpoint path to a file name
        /// </summary>
        /// <param name="endpoint">The endpoint path</param>
        /// <returns>A file name for the endpoint</returns>
        private static string GetFileNameFromEndpoint(string endpoint)
        {
            // Remove any path parameters
            endpoint = Regex.Replace(endpoint, @"\{[^}]+\}", string.Empty);
            
            // Split the endpoint path and get the last meaningful segment
            string[] segments = endpoint.Split('/', StringSplitOptions.RemoveEmptyEntries);
            string lastSegment = segments.Length > 0 ? segments[segments.Length - 1] : "Unknown";
            
            // Handle special cases
            if (lastSegment == "envelopes" && segments.Length == 1)
            {
                return "EnvelopesList";
            }
            else if (segments.Length > 1 && segments[0] == "envelopes" && string.IsNullOrEmpty(segments[1]))
            {
                return "EnvelopeResponse";
            }
            
            // Return a capitalized version of the last segment with "Response" appended
            return char.ToUpperInvariant(lastSegment[0]) + lastSegment.Substring(1) + "Response";
        }
    }
}