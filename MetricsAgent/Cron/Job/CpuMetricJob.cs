using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Model;
using Quartz;

namespace MetricsAgent.Cron.Job
{
    public class CpuMetricJob : IJob
    {
        private readonly ICPUMetricsRepository _repository;

        private readonly PerformanceCounter _counter;

        public CpuMetricJob(ICPUMetricsRepository repository)
        {
            _repository = repository;
            _counter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var value = Convert.ToInt32(_counter.NextValue());

            var time = DateTimeOffset.UtcNow;

            _repository.Create(new CpuMetric { Time = time, Value = value });
            
            return Task.CompletedTask;
        }
    }
}