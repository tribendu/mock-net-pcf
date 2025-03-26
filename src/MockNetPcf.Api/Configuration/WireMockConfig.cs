namespace MockNetPcf.Api.Configuration
{
    public class WireMockConfig
    {
        public int Port { get; set; } = 9090;
        public bool StartAdminInterface { get; set; } = true;
        public int AdminPort { get; set; } = 9091;
        public string[] AllowedOrigins { get; set; } = new[] { "*" };
        public string StoragePath { get; set; } = "mocks";
    }
}