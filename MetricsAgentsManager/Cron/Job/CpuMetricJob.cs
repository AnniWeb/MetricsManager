using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgentsManager.Client.Interfaces;
using MetricsAgentsManager.Client.Request;
using MetricsAgentsManager.Client.Response;
using MetricsAgentsManager.DAL.Interfaces;
using MetricsAgentsManager.DAL.Model;
using Quartz;

namespace MetricsAgentsManager.Cron.Job
{
    [DisallowConcurrentExecution]
    public class CpuMetricJob : IJob
    {
        private readonly ICPUMetricsRepository _metricsRepository;
        private readonly IAgentsRepository _agentsRepository;
        private readonly IMetricsClient _metricsClient;
        private readonly string _apiMethod = "metrics/cpu";

        public CpuMetricJob(ICPUMetricsRepository metricsRepository, IAgentsRepository agentsRepository, IMetricsClient metricsClient)
        {
            _metricsRepository = metricsRepository;
            _agentsRepository = agentsRepository;
            _metricsClient = metricsClient;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var agents = _agentsRepository.GetActiveAgents();

            // асинхронно собираем метрики
            var listMetrics = await Task.WhenAll(agents.Select(async agent =>
            {
                var toTime = DateTimeOffset.UtcNow;
                var fromTime = _metricsRepository.GetDateUpdate(agent.Id);
                var response = await GetMetricsByPeriod(agent, fromTime, toTime);
                return response.Select(metric => new CpuMetric()
                {
                    AgentId = agent.Id,
                    Time = metric.Time,
                    Value = metric.Value
                });
            }));

            // сохраняем метрики
            foreach (var metrics in listMetrics)
            {
                foreach (var metric in metrics)
                {
                    _metricsRepository.Create(metric);
                }
            }
        }

        /// <summary>
        /// Получение метрик от агента
        /// </summary>
        /// <param name="agent">агент</param>
        /// <param name="fromTime">начало период запращиваемых метрик</param>
        /// <param name="toTime">конец периода запращиваемых метрик</param>
        /// <returns>Список метрик</returns>
        private async Task<IList<CPUMetricResponse>> GetMetricsByPeriod(AgentInfo agent, DateTimeOffset fromTime,
            DateTimeOffset toTime)
        {
            var request = new GetMetricsByPeriodRequest
            {
                ApiHost = agent.Host,
                FromTime = fromTime,
                ToTime = toTime,
                ApiMethod = _apiMethod,
            };

            return await _metricsClient.GetMetricsByPeriod<ListCPUMetricsResponse, CPUMetricResponse>(request)
                .ConfigureAwait(false);
        }
    }
}
