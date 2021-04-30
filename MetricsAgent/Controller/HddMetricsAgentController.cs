using System;
using System.Collections.Generic;
using MetricsAgent.Entity;
using MetricsAgent.Model;
using MetricsAgent.Repository;
using MetricsAgent.Request;
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
    }
}