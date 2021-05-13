using System;
using System.Collections.Generic;
using MetricsAgent.Entity;
using MetricsAgent.Model;
using MetricsAgent.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controller
{
    [ApiController]
    [Route("api/metrics/ram")]
    public class RamMetricsAgentController : ControllerBase
    {
        /// <summary>
        /// Размер свободной оперативной памяти в мегабайтах
        /// </summary>
        /// <returns></returns>
        [HttpGet("left")]
        public ActionResult<double> GetAvailable ()
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
        public IEnumerable<RamMetrics> GetList ([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return new List<RamMetrics>();
        }
    }
}