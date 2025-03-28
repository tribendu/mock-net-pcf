namespace MockNetPcf.Api.Models
{
    /// <summary>
    /// Configuration options for the API recording service
    /// </summary>
    public class RecordingOptions
    {
        /// <summary>
        /// The target URL to record API interactions from
        /// </summary>
        public string TargetUrl { get; set; }
        
        /// <summary>
        /// Whether to save the recorded mappings to disk
        /// Default is true
        /// </summary>
        public bool SaveMapping { get; set; } = true;
        
        /// <summary>
        /// Optional file path to save the recorded mappings to
        /// If not specified, mappings will be saved to the default location
        /// </summary>
        public string SaveMappingToFile { get; set; }
    }
}