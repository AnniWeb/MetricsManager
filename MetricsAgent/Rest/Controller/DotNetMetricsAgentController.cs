using System;
using System.Collections.Generic;
using AutoMapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Rest.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Rest.Controller
{
    [ApiController]
    [Route("api/metrics/dotnet")]
    public class DotNetMetricsAgentController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsAgentController> _logger;
        private readonly IDotNetMetricsRepository _repository;
        private readonly IMapper _mapper;

        public DotNetMetricsAgentController(ILogger<DotNetMetricsAgentController> logger, IDotNetMetricsRepository repository, IMapper mapper)
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
            _logger.LogInformation("Запрос метрик за период");
            var metrics = _repository.GetByPeriod(fromTime, toTime);
            
            var response = new ListDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricResponse>()
            };
            
            if (metrics.Count > 0)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(_mapper.Map<DotNetMetricResponse>(metric));
                }
            }
            
            
            return Ok(response);
        }
    }
}