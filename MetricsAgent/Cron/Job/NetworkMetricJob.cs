using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Model;
using Quartz;

namespace MetricsAgent.Cron.Job
{
    public class NetworkMetricJob : IJob
    {
        private readonly INetworkMetricsRepository _repository;
        
        private readonly PerformanceCounter _counter;

        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;

            var category = new PerformanceCounterCategory("Network Interface");
            var instanceName = category.GetInstanceNames().First();

            _counter = new PerformanceCounter(category.CategoryName, "Bytes total/sec", instanceName);
        }

        public Task Execute(IJobExecutionContext context)
        {
            var value = Convert.ToInt32(_counter.NextValue());

            var time = DateTimeOffset.UtcNow;

            _repository.Create(new NetworkMetric { Time = time, Value = value });
            
            return Task.CompletedTask;
        }
    }
}
