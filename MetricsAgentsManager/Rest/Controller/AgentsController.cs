using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MetricsAgentsManager.DAL.Interfaces;
using MetricsAgentsManager.DAL.Model;
using MetricsAgentsManager.Rest.Request;
using MetricsAgentsManager.Rest.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgentsManager.Rest.Controller
{
    [Route("api/agents")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;
        private readonly IAgentsRepository _repository;
        private readonly IMapper _mapper;

        public AgentsController(ILogger<AgentsController> logger, IAgentsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, $"NLog встроен в {GetType()}");
            _repository = repository;
            _mapper = mapper;
        }
        
        
        /// <summary>
        /// Регистрация агента
        /// </summary>
        /// <param name="agentInfo">Данные агента</param>
        /// <returns></returns>
        /// <response code="201">Агент зарегистрирован</response>
        /// <response code="400">Переданы не корректные параетры</response> 
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfoRequest agentInfo)
        {
            _logger.LogInformation("Регистрация агента");
            
            _repository.Create(_mapper.Map<AgentInfo>(agentInfo));
            
            return Ok();
        }

        /// <summary>
        /// Включение агента
        /// </summary>
        /// <param name="agentId">Ид агента</param>
        /// <returns></returns>
        /// <response code="400">Переданы не корректные параетры</response> 
        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"Включение агента {agentId}");

            var agent = _repository.GetById(agentId);

            if (agent == null)
            {
                return NotFound();
            }

            if (!agent.Active)
            {
                agent.Active = true;
                _repository.Update(agent);
            }
            
            return Ok();
        }

        /// <summary>
        /// Отключение агента
        /// </summary>
        /// <param name="agentId">Ид агента</param>
        /// <returns></returns>
        /// <response code="400">Переданы не корректные параетры</response> 
        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"Отключение агента {agentId}");

            var agent = _repository.GetById(agentId);

            if (agent == null)
            {
                return NotFound();
            }

            if (agent.Active)
            {
                agent.Active = false;
                _repository.Update(agent);
            }
            
            return Ok();
        }

        /// <summary>
        /// Список зарегистрированных агентов
        /// </summary>
        /// <returns>Список агентов</returns>
        /// <response code="400">Переданы не корректные параетры</response> 
        [HttpGet]
        [ProducesResponseType(typeof(AgentInfoResponse), 200)]
        public IActionResult GetList()
        {
            _logger.LogInformation("Запрос списка агентов");
            
            var agents = _repository.GetAll();
            
            var response = new ListAgentInfoResponse()
            {
                Agents = new List<AgentInfoResponse>()
            };
            
            foreach (var agent in agents)
            {
                response.Agents.Add(_mapper.Map<AgentInfoResponse>(agent));
            }
            
            return Ok(response);
        }
        
        /// <summary>
        /// Данные метрик со всех агентов за период
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET agents/metrics/from/2021-01-01/to/2021-12-31
        ///
        /// </remarks>
        /// <param name="fromTime">начало периода выборки</param>
        /// <param name="toTime">конец периода выборки</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="400">Переданы не корректные параетры</response> 
        [HttpGet("metrics/from/{fromTime}/to/{toTime}")]
        [ProducesResponseType(typeof(ListAllMetricsResponse), 200)]
        public IActionResult GetByPeriod ([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос метрик за период c {fromTime:f} по {toTime:f}");

            var response = new ListAllMetricsResponse()
            {
                CpuMetrics = new List<CPUMetricResponse>(),
                NetworkMetrics = new List<NetworkMetricResponse>(),
                DotNetMetrics = new List<DotNetMetricResponse>(),
                HddSpaceMetrics = new List<HddSpaceMetricResponse>(),
                RamMetrics = new List<RamMetricResponse>()
            };

            response.CpuMetrics = _repository.GetCpuMetricsByPeriod(fromTime, toTime)
                .Select(metric => _mapper.Map<CPUMetricResponse>(metric));
            response.NetworkMetrics = _repository.GetNetworkMetricsByPeriod(fromTime, toTime)
                .Select(metric => _mapper.Map<NetworkMetricResponse>(metric));
            response.DotNetMetrics = _repository.GetDotNetMetricsByPeriod(fromTime, toTime)
                .Select(metric => _mapper.Map<DotNetMetricResponse>(metric));
            response.HddSpaceMetrics = _repository.GetHddSpaceMetricsByPeriod(fromTime, toTime)
                .Select(metric => _mapper.Map<HddSpaceMetricResponse>(metric));
            response.RamMetrics = _repository.GetRamMetricsByPeriod(fromTime, toTime)
                .Select(metric => _mapper.Map<RamMetricResponse>(metric));
            
            return Ok(response);
        }

        /// <summary>
        /// Данные метрик с агента за период
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     GET agents/1/metrics/from/2021-01-01/to/2021-12-31
        /// 
        /// </remarks>
        /// <param name="agentId"></param>
        /// <param name="fromTime">начало периода выборки</param>
        /// <param name="toTime">конец периода выборки</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="400">Переданы не корректные параетры</response> 
        [HttpGet("{agentId}/metrics/from/{fromTime}/to/{toTime}")]
        [ProducesResponseType(typeof(ListAllMetricsResponse), 200)]
        public IActionResult GetByPeriod ([FromRoute] long agentId, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос метрик за период c {fromTime:f} по {toTime:f}");

            var response = new ListAllMetricsResponse()
            {
                CpuMetrics = new List<CPUMetricResponse>(),
                NetworkMetrics = new List<NetworkMetricResponse>(),
                DotNetMetrics = new List<DotNetMetricResponse>(),
                HddSpaceMetrics = new List<HddSpaceMetricResponse>(),
                RamMetrics = new List<RamMetricResponse>()
            };

            response.CpuMetrics = _repository.GetCpuMetricsByPeriod(fromTime, toTime, agentId)
                .Select(metric => _mapper.Map<CPUMetricResponse>(metric));
            response.NetworkMetrics = _repository.GetNetworkMetricsByPeriod(fromTime, toTime, agentId)
                .Select(metric => _mapper.Map<NetworkMetricResponse>(metric));
            response.DotNetMetrics = _repository.GetDotNetMetricsByPeriod(fromTime, toTime, agentId)
                .Select(metric => _mapper.Map<DotNetMetricResponse>(metric));
            response.HddSpaceMetrics = _repository.GetHddSpaceMetricsByPeriod(fromTime, toTime, agentId)
                .Select(metric => _mapper.Map<HddSpaceMetricResponse>(metric));
            response.RamMetrics = _repository.GetRamMetricsByPeriod(fromTime, toTime, agentId)
                .Select(metric => _mapper.Map<RamMetricResponse>(metric));
            
            return Ok(response);
        }
    }
}