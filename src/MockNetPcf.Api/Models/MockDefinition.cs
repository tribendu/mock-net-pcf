using System.Collections.Generic;

namespace MockNetPcf.Api.Models
{
    /// <summary>
    /// Represents a mock API endpoint definition
    /// </summary>
    public class MockDefinition
    {
        /// <summary>
        /// Unique identifier for the mock definition
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Descriptive name for the mock endpoint
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// URL path that this mock will respond to
        /// </summary>
        public string Path { get; set; }
        
        /// <summary>
        /// HTTP method this mock will respond to (GET, POST, PUT, DELETE, etc.)
        /// </summary>
        public string Method { get; set; }
        
        /// <summary>
        /// Headers that must be present in the request to match this mock
        /// </summary>
        public Dictionary<string, string> RequestHeaders { get; set; }
        
        /// <summary>
        /// Expected request body content to match this mock
        /// </summary>
        public string RequestBody { get; set; }
        
        /// <summary>
        /// HTTP status code to return in the response
        /// </summary>
        public int StatusCode { get; set; }
        
        /// <summary>
        /// Headers to include in the mock response
        /// </summary>
        public Dictionary<string, string> ResponseHeaders { get; set; }
        
        /// <summary>
        /// Body content to return in the mock response
        /// </summary>
        public string ResponseBody { get; set; }
        
        /// <summary>
        /// Optional error message to include when returning error status codes
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}