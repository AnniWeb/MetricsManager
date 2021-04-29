using System;
using System.Collections.Generic;
using MetricsAgent.Entity;
using MetricsAgent.Model;
using MetricsAgent.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controller
{
    [ApiController]
    [Route("api/metrics/dotnet")]
    public class DotNetMetricsAgentController : ControllerBase
    {
        
        /// <summary>
        /// Колличество ошибок за период
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public ActionResult<int> GetErrorsCount ([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return 1;
        }
    }
}