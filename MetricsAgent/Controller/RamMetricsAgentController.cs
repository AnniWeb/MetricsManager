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
    }
}