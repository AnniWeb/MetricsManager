using System.Collections.Generic;

namespace MetricsAgentsManager.Client.Interfaces
{
    public interface IListMetricsResponse <T>
    {
        IList<T> Metrics { get; set;  }
    }
}