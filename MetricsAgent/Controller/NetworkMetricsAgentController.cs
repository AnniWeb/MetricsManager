using System;
using System.Collections.Generic;
using MetricsAgent.Entity;
using MetricsAgent.Model;
using MetricsAgent.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controller
{
    [ApiController]
    [Route("api/metrics/network")]
    public class NetworkMetricsAgentController : ControllerBase
    {
        /// <summary>
        /// Данные метрики за период
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IEnumerable<NetworkMetricsModel> GetList ([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return new List<NetworkMetricsModel>();
        }
    }
}