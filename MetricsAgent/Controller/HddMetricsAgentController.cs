using System;
using System.Collections.Generic;
using MetricsAgent.Entity;
using MetricsAgent.Model;
using MetricsAgent.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controller
{
    [ApiController]
    [Route("api/metrics/hdd")]
    public class HddMetricsAgentController : ControllerBase
    {
        /// <summary>
        /// Размер оставшегося свободного дискового пространства в мегабайтах
        /// </summary>
        /// <returns></returns>
        [HttpGet("left")]
        public ActionResult<double> GetLeftSpace ()
        {
            return 0;
        }
        
        /// <summary>
        /// Данные метрики за период
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IEnumerable<HddSpaceMetrics> GetList ([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return new List<HddSpaceMetrics>();
        }
    }
}