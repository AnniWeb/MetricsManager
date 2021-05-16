using MetricsAgentsManager.Model;
using MetricsAgentsManager.Controller;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;


namespace MetricsAgentsManagerTests
{
    public class AgentsManagerTests
    {
        private readonly AgentsController _controller;
        private readonly Uri _root;

        public AgentsManagerTests()
        {
            var logger = new Mock<ILogger<AgentsController>>();
            
            _controller = new AgentsController(logger.Object);
            _root = new Uri("https://localhost:44347");
        }
        
        [Fact]
        public void TestRegisterAgent_ReturnOk()
        {
            //Arrange
            var agent = new AgentInfo { AgentAddress = _root };
            
            //Act
            var result = _controller.RegisterAgent(agent);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        
        [Fact]
        public void TestEnableAgentById_ReturnOk()
        {
            //Arrange
            var agentId = 1;
            
            //Act
            var result = _controller.EnableAgentById(agentId);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        
        [Fact]
        public void TestDisableAgentById_ReturnOk()
        {
            //Arrange
            var agentId = 1;
            
            //Act
            var result = _controller.DisableAgentById(agentId);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        
        [Fact]
        public void TestGetList_ReturnList()
        {
            //Arrange
            
            //Act
            var result = _controller.GetList();

            // Assert
            _ = Assert.IsAssignableFrom<ActionResult<IEnumerable<AgentInfo>>>(result);
        }
    }
}