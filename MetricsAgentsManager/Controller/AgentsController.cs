using System;
using System.Collections.Generic;
using MetricsAgentsManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgentsManager.Controller
{
    [Route("api/agents")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;

        public AgentsController(ILogger<AgentsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, $"NLog встроен в {GetType()}");
        }
        
        
        /// <summary>
        /// Регистрация агента
        /// </summary>
        /// <param name="agentInfo"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _logger.LogInformation("Регистрация агента");
            return Ok();
        }

        /// <summary>
        /// Включение агента
        /// </summary>
        /// <param name="agentId"></param>
        /// <returns></returns>
        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"Включение агента {agentId}");
            return Ok();
        }

        /// <summary>
        /// Отключение агента
        /// </summary>
        /// <param name="agentId"></param>
        /// <returns></returns>
        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"Отключение агента {agentId}");
            return Ok();
        }

        /// <summary>
        /// Список зарегистрированных агентов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<AgentInfo>> GetList()
        {
            _logger.LogInformation("Запрос списка агентов");
            return new List<AgentInfo>();
        }
    }
}