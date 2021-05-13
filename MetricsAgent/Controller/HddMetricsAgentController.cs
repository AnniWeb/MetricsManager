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
    [Route("api/metrics/hdd")]
    public class HddMetricsAgentController : ControllerBase
    {
        private readonly ILogger<HddMetricsAgentController> _logger;
        private readonly IHddMetricsRepository _repository;

        public HddMetricsAgentController(ILogger<HddMetricsAgentController> logger, IHddMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, $"NLog встроен в {GetType()}");
            
            _repository = repository;
            _repository.CreateTable();
        }
        
        /// <summary>
        /// Размер оставшегося свободного дискового пространства в мегабайтах
        /// </summary>
        /// <returns></returns>
        [HttpGet("left")]
        public ActionResult<double> GetLeftSpace ()
        {
            _logger.LogInformation("Запрос размера свободного дискового пространства в мегабайтах");
            
            var metric = _repository.GetLast();
            var leftMb = metric.Value / 1024 / 1024;
            return Ok(leftMb);
        }

        [HttpPost]
        public IActionResult Create([FromBody] HddMetricsRequest metricRequest)
        {
            _repository.Create(new HddSpaceMetricsModel()
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
            
            var response = new ListHddSpaceMetricsResponse()
            {
                Metrics = new List<HddSpaceMetricResponse>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new HddSpaceMetricResponse()
                {
                    Id = metric.Id,
                    Value = metric.Value,
                    Time = metric.Time
                });
            }
            
            return Ok(response);
        }
    }
}