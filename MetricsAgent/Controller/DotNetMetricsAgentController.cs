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
    [Route("api/metrics/dotnet")]
    public class DotNetMetricsAgentController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsAgentController> _logger;
        private readonly IDotNetMetricsRepository _repository;

        public DotNetMetricsAgentController(ILogger<DotNetMetricsAgentController> logger, IDotNetMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, $"NLog встроен в {GetType()}");
            
            _repository = repository;
            _repository.CreateTable();
        }
        
        /// <summary>
        /// Количество ошибок за период
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public ActionResult<int> GetErrorsCount ([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            var metrics = _repository.GetByPeriod(fromTime, toTime);
            _logger.LogInformation("Запрос количества ошибок за период");
            return Ok(metrics == null ? 0 : metrics.Count);
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] DotNetMetricRequest metricRequest)
        {
            _repository.Create(new DotNetMetricsModel()
            {
                Time = metricRequest.Time,
                Value = metricRequest.Value
            });
            
            return Ok();
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
            
            var response = new ListDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricResponse>()
            };
            
            if (metrics.Count > 0)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(new DotNetMetricResponse()
                    {
                        Id = metric.Id,
                        Value = metric.Value,
                        Time = metric.Time
                    });
                }
            }
            
            
            return Ok(response);
        }
    }
}