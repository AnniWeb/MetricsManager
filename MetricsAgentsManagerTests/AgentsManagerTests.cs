using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using AutoMapper;
using MetricsAgentsManager.DAL.Interfaces;
using MetricsAgentsManager.DAL.Model;
using MetricsAgentsManager.Rest.Controller;
using MetricsAgentsManager.Rest.Request;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using MetricsAgentsManager.Rest.Response;

namespace MetricsAgentsManagerTests
{
    public class AgentsManagerTests
    {
        private readonly AgentsController _controller;
        private readonly Uri _root;
        private readonly Mock<IAgentsRepository> _mock;

        public AgentsManagerTests()
        {
            _mock = new Mock<IAgentsRepository>();
            var logger = new Mock<ILogger<AgentsController>>();
            var mapper = new Mock<IMapper>();
            
            _controller = new AgentsController(logger.Object, _mock.Object, mapper.Object);
            _root = new Uri("https://localhost:44347");
        }
        
        [Fact]
        public void TestRegisterAgent_ReturnOk()
        {
            //Arrange
            var agent = new AgentInfoRequest() { Host = _root.ToString() };
            
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
            _mock.Setup(repo => repo.GetAll()).Returns(new List<AgentInfo>());
            
            //Act
            var result = _controller.GetList();

            // Assert
            var okResult = result as ObjectResult;
            Assert.NotNull(okResult);
            Assert.IsType<ListAgentInfoResponse>(okResult.Value);
        }
        
        [Fact]
        public void TestGetListMetrics_ReturnList()
        {
            //Arrange
            _mock.Setup(repo => repo.GetCpuMetricsByPeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>()))
                .Returns(new List<CpuMetric>());
            _mock.Setup(repo => repo.GetNetworkMetricsByPeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>()))
                .Returns(new List<NetworkMetric>());
            _mock.Setup(repo => repo.GetDotNetMetricsByPeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>()))
                .Returns(new List<DotNetMetric>());
            _mock.Setup(repo => repo.GetHddSpaceMetricsByPeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>()))
                .Returns(new List<HddSpaceMetric>());
            _mock.Setup(repo => repo.GetRamMetricsByPeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>()))
                .Returns(new List<RamMetric>());
            var fromTime = DateTimeOffset.Now.AddDays(-5);
            var toTime = DateTimeOffset.Now;
            
            //Act
            var result = _controller.GetByPeriod(fromTime, toTime);

            // Assert
            var okResult = result as ObjectResult;
            Assert.NotNull(okResult);
            Assert.IsType<ListAllMetricsResponse>(okResult.Value);
        }
    }
}