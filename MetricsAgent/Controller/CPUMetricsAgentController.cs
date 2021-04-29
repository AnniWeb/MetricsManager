using System;
using System.Collections.Generic;
using MetricsAgent.Entity;
using MetricsAgent.Model;
using MetricsAgent.Repository;
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

        public CPUMetricsAgentController(ILogger<CPUMetricsAgentController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, $"NLog встроен в {GetType()}");
        }
        
        /// <summary>
        /// Данные метрики за период с процентилем
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <param name="percentile"></param>
        /// <returns></returns>
        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IEnumerable<CPUMetricsModel> GetList ([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime, [FromRoute] Percentile percentile)
        {
            _logger.LogInformation($"Запрос метрик за период с процентилем {percentile.GetDisplayName()}");
            return new List<CPUMetricsModel>();
        }
        
        /// <summary>
        /// Данные метрики за период
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IEnumerable<CPUMetricsModel> GetList ([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Запрос метрик за период");
            return new List<CPUMetricsModel>();
        }
    }
}