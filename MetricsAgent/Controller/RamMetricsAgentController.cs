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
    [Route("api/metrics/ram")]
    public class RamMetricsAgentController : ControllerBase
    {
        private readonly ILogger<RamMetricsAgentController> _logger;
        private readonly IRamMetricsRepository _repository;

        public RamMetricsAgentController(ILogger<RamMetricsAgentController> logger, IRamMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, $"NLog встроен в {GetType()}");
            
            _repository = repository;
            _repository.CreateTable();
        }
        
        /// <summary>
        /// Размер свободной оперативной памяти в мегабайтах
        /// </summary>
        /// <returns></returns>
        [HttpGet("left")]
        public ActionResult<double> GetAvailable ()
        {
            _logger.LogInformation("Запрос размера свободной оперативной памяти в мегабайтах");
            var metric = _repository.GetLast();
            var leftMb = metric.Value / 1024 / 1024;
            return Ok(leftMb);
        }
        
        
        [HttpPost]
        public IActionResult Create([FromBody] RamMetricRequest metricRequest)
        {
            _repository.Create(new RamMetricsModel()
            {
                Time = metricRequest.Time,
                Value = metricRequest.Value
            });
            
            return Ok();
        }
    }
}