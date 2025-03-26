namespace MockNetPcf.Api.Models
{
    public class RecordingOptions
    {
        public string TargetUrl { get; set; }
        public bool SaveMapping { get; set; } = true;
        public string SaveMappingToFile { get; set; }
    }
}