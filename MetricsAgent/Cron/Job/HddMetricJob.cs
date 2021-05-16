using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Model;
using Quartz;

namespace MetricsAgent.Cron.Job
{
    public class HddMetricJob : IJob
    {
        private readonly IHddMetricsRepository _repository;

        public HddMetricJob(IHddMetricsRepository repository)
        {
            _repository = repository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var disc = DriveInfo.GetDrives().First();
            var value = disc.AvailableFreeSpace;

            var time = DateTimeOffset.UtcNow;

            _repository.Create(new HddSpaceMetric { Time = time, Value = value });
            
            return Task.CompletedTask;
        }
        
    }
}