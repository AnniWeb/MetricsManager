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
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET metrics/dotnet/from/2021-01-01/to/2021-12-31
        ///
        /// </remarks>
        /// <param name="fromTime">начало периода выборки</param>
        /// <param name="toTime">конец периода выборки</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="400">Переданы не корректные параетры</response> 
        [HttpGet("from/{fromTime}/to/{toTime}")]
        [ProducesResponseType(typeof(ListDotNetMetricsResponse), 200)]
        public IActionResult GetByPeriod ([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос метрик за период c {fromTime:f} по {toTime:f}");
            var metrics = _repository.GetByPeriod(fromTime, toTime);
            
            var response = new ListDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricResponse>()
            };
            
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricResponse>(metric));
            }
            
            return Ok(response);
        }
    }
}