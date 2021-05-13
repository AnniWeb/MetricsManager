using System;
using System.Collections.Generic;
using MetricsAgent.Entity;
using MetricsAgent.Model;
using MetricsAgent.Repository;
using MetricsAgent.Request;
using MetricsAgent.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controller
{
    [ApiController]
    [Route("api/metrics/network")]
    public class NetworkMetricsAgentController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsAgentController> _logger;
        private readonly INetworkMetricsRepository _repository;

        public NetworkMetricsAgentController(ILogger<NetworkMetricsAgentController> logger, INetworkMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, $"NLog встроен в {GetType()}");
            
            _repository = repository;
            _repository.CreateTable();
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
            
            var response = new ListNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricResponse>()
            };

            if (metrics.Count > 0)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(new NetworkMetricResponse()
                    {
                        Id = metric.Id,
                        Value = metric.Value,
                        Time = metric.Time
                    });
                }
            }
            
            return Ok(response);
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] NetworkMetricRequest metricRequest)
        {
            _repository.Create(new NetworkMetricsModel()
            {
                Time = metricRequest.Time,
                Value = metricRequest.Value
            });
            
            return Ok();
        }
    }
}