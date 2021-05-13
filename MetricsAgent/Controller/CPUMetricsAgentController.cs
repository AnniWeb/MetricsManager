using System;
using System.Collections.Generic;
using MetricsAgent.Entity;
using MetricsAgent.Model;
using MetricsAgent.Repository;
using MetricsAgent.Request;
using MetricsAgent.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Extensions;

namespace MetricsAgent.Controller
{
    [ApiController]
    [Route("api/metrics/cpu")]
    public class CPUMetricsAgentController : ControllerBase
    {
        private readonly ILogger<CPUMetricsAgentController> _logger;
        private readonly ICPUMetricsRepository _repository;

        public CPUMetricsAgentController(ILogger<CPUMetricsAgentController> logger, ICPUMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, $"NLog встроен в {GetType()}");
            
            _repository = repository;
            _repository.CreateTable();
        }
        
        /// <summary>
        /// Данные метрики за период с процентилем
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <param name="percentile"></param>
        /// <returns></returns>
        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetList ([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime, [FromRoute] Percentile percentile)
        {
            _logger.LogInformation($"Запрос метрик за период с процентилем {percentile.GetDisplayName()}");
            var metrics = _repository.GetByPeriod(fromTime, toTime, percentile);
            
            var response = new ListCPUMetricsResponse()
            {
                Metrics = new List<CPUMetricResponse>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new CPUMetricResponse()
                {
                    Id = metric.Id,
                    Value = metric.Value,
                    Time = metric.Time
                });
            }
            
            return Ok(response);
        }
        
        /// <summary>
        /// Данные метрики за период
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetList ([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Запрос метрик за период");
            var metrics = _repository.GetByPeriod(fromTime, toTime);
            
            var response = new ListCPUMetricsResponse()
            {
                Metrics = new List<CPUMetricResponse>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new CPUMetricResponse()
                {
                    Id = metric.Id,
                    Value = metric.Value,
                    Time = metric.Time
                });
            }
            
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CPUMetricRequest metricRequest)
        {
            _repository.Create(new CPUMetricsModel()
            {
                Time = metricRequest.Time,
                Value = metricRequest.Value
            });
            
            return Ok();
        }
    }
}