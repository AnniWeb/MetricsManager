using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Model;
using Quartz;

namespace MetricsAgent.Cron.Job
{
    public class DotNetMetricJob : IJob
    {
        private readonly IDotNetMetricsRepository _repository;
        
        private readonly PerformanceCounter _counter;

        public DotNetMetricJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;

            _counter = new PerformanceCounter("ASP.NET", "Worker Processes Running");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var value = Convert.ToInt32(_counter.NextValue());
            
            var time = DateTimeOffset.UtcNow;
            
            _repository.Create(new DotNetMetric { Time = time, Value = value });
            
            return Task.CompletedTask;
        }
    }
}
