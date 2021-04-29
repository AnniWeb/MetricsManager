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
    [Route("api/metrics/dotnet")]
    public class DotNetMetricsAgentController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsAgentController> _logger;

        public DotNetMetricsAgentController(ILogger<DotNetMetricsAgentController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, $"NLog встроен в {GetType()}");
        }
        
        /// <summary>
        /// Количество ошибок за период
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public ActionResult<int> GetErrorsCount ([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Запрос количества ошибок за период");
            return 1;
        }
    }
}