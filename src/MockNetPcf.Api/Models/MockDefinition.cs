using System.Collections.Generic;

namespace MockNetPcf.Api.Models
{
    public class MockDefinition
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Method { get; set; }
        public Dictionary<string, string> RequestHeaders { get; set; }
        public string RequestBody { get; set; }
        public int StatusCode { get; set; }
        public Dictionary<string, string> ResponseHeaders { get; set; }
        public string ResponseBody { get; set; }
    }
}