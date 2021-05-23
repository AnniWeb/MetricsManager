using System.Collections.Generic;
using System.Threading.Tasks;
using MetricsAgentsManager.Client.Request;

namespace MetricsAgentsManager.Client.Interfaces
{
    public interface IMetricsClient
    {
        Task<IList<TRe>> GetMetricsByPeriod<TRl, TRe>(GetMetricsByPeriodRequest request)
            where TRl : IListMetricsResponse<TRe> where TRe : class;
    }
}