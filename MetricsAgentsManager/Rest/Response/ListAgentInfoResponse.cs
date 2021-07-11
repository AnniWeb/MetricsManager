using System.Collections.Generic;

namespace MetricsAgentsManager.Rest.Response
{
    public class ListAgentInfoResponse
    {
        public IList<AgentInfoResponse> Agents { get; set; }
    }
}