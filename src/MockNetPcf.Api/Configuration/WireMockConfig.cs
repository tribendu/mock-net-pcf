namespace MockNetPcf.Api.Configuration
{
    /// <summary>
    /// Configuration settings for the WireMock.NET server
    /// </summary>
    public class WireMockConfig
    {
        /// <summary>
        /// The port on which the mock server will listen for requests
        /// Default is 9090
        /// </summary>
        public int Port { get; set; } = 9090;
        
        /// <summary>
        /// Whether to start the WireMock admin interface
        /// Default is true
        /// </summary>
        public bool StartAdminInterface { get; set; } = true;
        
        /// <summary>
        /// The port on which the admin interface will be available
        /// Default is 9091
        /// </summary>
        public int AdminPort { get; set; } = 9091;
        
        /// <summary>
        /// CORS allowed origins for the mock server
        /// Default is all origins (*)
        /// </summary>
        public string[] AllowedOrigins { get; set; } = new[] { "*" };
        
        /// <summary>
        /// Path where mock mappings will be stored
        /// Default is "mocks" directory
        /// </summary>
        public string StoragePath { get; set; } = "mocks";
    }
}