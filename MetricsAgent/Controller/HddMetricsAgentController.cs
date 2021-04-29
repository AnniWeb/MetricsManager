using System;
using System.Collections.Generic;
using MetricsAgent.Entity;
using MetricsAgent.Model;
using MetricsAgent.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controller
{
    [ApiController]
    [Route("api/metrics/hdd")]
    public class HddMetricsAgentController : ControllerBase
    {
        private readonly ILogger<HddMetricsAgentController> _logger;

        public HddMetricsAgentController(ILogger<HddMetricsAgentController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, $"NLog встроен в {GetType()}");
        }
        
        /// <summary>
        /// Размер оставшегося свободного дискового пространства в мегабайтах
        /// </summary>
        /// <returns></returns>
        [HttpGet("left")]
        public ActionResult<double> GetLeftSpace ()
        {
            _logger.LogInformation("Запрос размера свободного дискового пространства в мегабайтах");
            return 0;
        }
    }
}