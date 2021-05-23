using System.Collections.Generic;

namespace MetricsAgent.Rest.Interfaces
{
    public interface IListMetricsResponse <T>
    {
        IList<T> Metrics { get; set;  }
    }
}