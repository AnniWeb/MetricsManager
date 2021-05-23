using AutoMapper;
using MetricsAgentsManager.DAL.Model;
using MetricsAgentsManager.Rest.Response;
using MetricsAgentsManager.Rest.Request;
using Microsoft.AspNetCore.Http;

namespace MetricsAgentsManager.DAL.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AgentInfoRequest, AgentInfo>();
            CreateMap<AgentInfo, AgentInfoResponse>();
            
            CreateMap<CpuMetric, CPUMetricResponse>();
            CreateMap<DotNetMetric, DotNetMetricResponse>();
            CreateMap<HddSpaceMetric, HddSpaceMetricResponse>();
            CreateMap<NetworkMetric, NetworkMetricResponse>();
            CreateMap<RamMetric, RamMetricResponse>();
        }
    }
}