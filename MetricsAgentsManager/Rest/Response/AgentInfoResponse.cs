using Microsoft.AspNetCore.Http;

namespace MetricsAgentsManager.Rest.Response
{
    public class AgentInfoResponse
    {
        public int Id { get; set; }

        public string Host { get; set; }
        
        public bool Active { get; set; }
    }
}