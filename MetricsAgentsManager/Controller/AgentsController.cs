using System;
using System.Collections.Generic;
using MetricsAgentsManager.Model;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentsManager.Controller
{
    [Route("api/agents")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        /// <summary>
        /// Регистрация агента
        /// </summary>
        /// <param name="agentInfo"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
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
            return Ok();
        }

        /// <summary>
        /// Список зарегистрированных агентов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<AgentInfo>> GetList()
        {
            return new List<AgentInfo>();
        }
    }
}