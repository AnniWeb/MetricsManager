using System;
using System.Collections.Generic;
using MetricsAgent.Entity;
using MetricsAgent.Model;
using MetricsAgent.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controller
{
    [ApiController]
    [Route("api/metrics/cpu")]
    public class CPUMetricsAgentController : ControllerBase
    {
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
            return new List<CPUMetricsModel>();
        }
    }
}