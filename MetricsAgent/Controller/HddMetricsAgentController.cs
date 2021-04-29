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
    }
}