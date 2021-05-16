using AutoMapper;
using MetricsAgent.DAL.Model;
using MetricsAgent.Rest.Request;
using MetricsAgent.Rest.Response;

namespace MetricsAgent.DAL.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CPUMetricRequest, CpuMetric>();
            CreateMap<CpuMetric, CPUMetricResponse>();
            
            CreateMap<DotNetMetricRequest, DotNetMetric>();
            CreateMap<DotNetMetric, DotNetMetricResponse>();
            
            CreateMap<HddMetricRequest, HddSpaceMetric>();
            CreateMap<HddSpaceMetric, HddSpaceMetricResponse>();
            
            CreateMap<NetworkMetricRequest, NetworkMetric>();
            CreateMap<NetworkMetric, NetworkMetricResponse>();
            
            CreateMap<RamMetricRequest, RamMetric>();
            CreateMap<RamMetric, RamMetricResponse>();
        }
    }
}