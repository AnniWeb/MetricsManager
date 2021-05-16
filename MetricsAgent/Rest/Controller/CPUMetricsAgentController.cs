using System;
using System.Collections.Generic;
using AutoMapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Model;
using MetricsAgent.Rest.Request;
using MetricsAgent.Rest.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Rest.Controller
{
    [ApiController]
    [Route("api/metrics/cpu")]
    public class CpuMetricsAgentController : ControllerBase
    {
        private readonly ILogger<CpuMetricsAgentController> _logger;
        private readonly ICPUMetricsRepository _repository;
        private readonly IMapper _mapper;

        public CpuMetricsAgentController(ILogger<CpuMetricsAgentController> logger, ICPUMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, $"NLog встроен в {GetType()}");
            
            _repository = repository;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Данные метрики за период
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetByPeriod ([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос метрик за период c {fromTime:f} по {toTime:f}");
            var metrics = _repository.GetByPeriod(fromTime, toTime);
            
            var response = new ListCPUMetricsResponse()
            {
                Metrics = new List<CPUMetricResponse>()
            };
            
            if (metrics.Count > 0)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(_mapper.Map<CPUMetricResponse>(metric));
                }
            }
            
            return Ok(response);
        }
    }
}