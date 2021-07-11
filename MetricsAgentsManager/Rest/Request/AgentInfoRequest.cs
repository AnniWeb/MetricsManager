using Microsoft.AspNetCore.Http;

namespace MetricsAgentsManager.Rest.Request
{
    public class AgentInfoRequest
    {
        public string Host { get; set; }
        
        public bool Active { get; set; }
    }
}