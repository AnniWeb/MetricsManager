using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Model;
using Quartz;

namespace MetricsAgent.Cron.Job
{
    public class RamMetricJob : IJob
    {
        private readonly IRamMetricsRepository _repository;
        
        private readonly PerformanceCounter _counter;

        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            _counter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var value = Convert.ToInt32(_counter.NextValue());

            var time = DateTimeOffset.UtcNow;

            _repository.Create(new RamMetric { Time = time, Value = value });
            
            return Task.CompletedTask;
        }
    }
}
