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
    [Route("api/metrics/ram")]
    public class RamMetricsAgentController : ControllerBase
    {
        private readonly ILogger<RamMetricsAgentController> _logger;

        public RamMetricsAgentController(ILogger<RamMetricsAgentController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, $"NLog встроен в {GetType()}");
        }
        
        /// <summary>
        /// Размер свободной оперативной памяти в мегабайтах
        /// </summary>
        /// <returns></returns>
        [HttpGet("left")]
        public ActionResult<double> GetAvailable ()
        {
            _logger.LogInformation("Запрос размера свободной оперативной памяти в мегабайтах");
            return 0;
        }
    }
}